<template>
    <div class="content__navbar" tabindex="0" @keydown="handleSearchByKeyBoard">
        <div class="content__navbar-left">
            <div class="text-field">
                <m-text-input
                    v-model="this.searchInput"
                    :placeholder="this.$_MISAResource['vn-VI'].searchProperty"
                    individualClass="text__input-icon-start"
                    ref="inputSearch"
                ></m-text-input>
            </div>
            <el-select
                class="back-ground__icon"
                v-model="propertyTypeFilter"
                clearable
                :placeholder="this.$_MISAResource['vn-VI'].propertyType"
                filterable
                :title="propertyTypeFilter"
                :no-match-text="'Không có kết quả'"
            >
                <el-option
                    v-for="item in PropertyTypes"
                    :key="item.PropertyTypeId"
                    :label="item.PropertyTypeName"
                    :value="item.PropertyTypeName"
                    :title="item.PropertyTypeName"
                ></el-option>
            </el-select>
            <el-select
                v-model="departmentUseFilter"
                clearable
                :placeholder="this.$_MISAResource['vn-VI'].departmentUse"
                class="back-ground__icon"
                filterable
                :title="departmentUseFilter"
                :no-match-text="'Không có kết quả'"
            >
                <el-option
                    v-for="item in Departments"
                    :key="item.DepartmentId"
                    :label="item.DepartmentName"
                    :value="item.DepartmentName"
                    :title="item.DepartmentName"
                ></el-option>
            </el-select>
            <m-button
                :label="this.$_MISAResource['vn-VI'].reload"
                class="content__btn--reload"
                :individualClass="'btn btn--noborder'"
                :icon="'icon--reload'"
                @click="
                    async () => {
                        await this.getPropertyWithFilter();
                    }
                "
            ></m-button>
        </div>
        <div class="content__navbar-right">
            <m-button
                :label="this.$_MISAResource['vn-VI'].addProperties"
                :individualClass="'btn--primary'"
                @click="this.showAddProperty"
            ></m-button>
            <m-button
                v-tippy="$_MISAResource['vn-VI'].exportExcel"
                :individualClass="' btn--single btn--combo-excel'"
            ></m-button>
            <m-button
                v-tippy="$_MISAResource['vn-VI'].delete"
                :individualClass="' btn--single'"
                :icon="'icon--delete'"
                @click="this.deleteRowOnClickIcon"
            ></m-button>
            <div class="drop-down">
                <m-button
                    v-tippy="$_MISAResource['vn-VI'].hide"
                    :individualClass="' btn--single btn--setting'"
                    :icon="'icon--setting'"
                    @click.stop="this.toggleMiniSetting"
                ></m-button>
                <m-setting
                    v-show="this.isShowMiniSetting"
                    :isShowMiniSetting="this.isShowMiniSetting"
                    @toggleMiniSetting="this.toggleMiniSetting"
                ></m-setting>
            </div>
        </div>
    </div>
    <div class="content__body">
        <div class="table__content">
            <div class="table__content--header">
                <div
                    class="table__content--header-item cell--item"
                    :class="{ 'cell--item--checkbox': header.name == 'checkbox' }"
                    v-for="(header, index) in this.listHeader"
                    :key="index"
                    :style="{ minWidth: header.width }"
                >
                    <template v-if="header.name === 'checkbox'">
                        <m-checkbox
                            ref="checkbox-all"
                            type="primary"
                            :class="header.align"
                            @click="clickOnCheckBoxAll"
                        ></m-checkbox>
                    </template>
                    <template v-else>
                        <el-tooltip
                            v-if="header.fullName"
                            effect="dark"
                            :content="header.fullName"
                            placement="bottom-start"
                        >
                            <div :class="header.align">{{ header.name }}</div>
                        </el-tooltip>
                        <div v-else :class="header.align">
                            {{ header.name }}
                        </div>
                    </template>
                </div>
            </div>
            <div class="table__content--body" style="width: 1670px" ref="contentBody" @scroll="scrollHandler($event)">
                <div
                    v-for="(data, index) in this.dataRender"
                    :key="data.Id"
                    :class="{
                        'row-selected': isSelected(data.PropertyId),
                        'row-hovered': this.hoveredRowIndex == data.PropertyId || this.indexContextMenu == index,
                    }"
                    @click="clickOnRowTable(index, data.PropertyId, $event)"
                    class="table__content--row"
                    @mouseenter.stop="handleMouseOver(data.PropertyId)"
                    @mouseleave="handleMouseOut"
                    @dblclick="showDetail(index, data.PropertyId)"
                    @contextmenu.prevent="showContextMenu($event, index, data.PropertyId)"
                >
                    <div class="text-align-center cell--item cell--item--checkbox" style="minWidth: 50px">
                        <m-checkbox
                            :ref="`checkbox-${data.PropertyId}`"
                            :id="data.PropertyId"
                            @click="clickOnCheckbox(index, data.PropertyId)"
                        ></m-checkbox>
                    </div>
                    <div class="text-align-center cell--item" style="minWidth: 50px">{{ index + 1 }}</div>
                    <div class="text-align-left cell--item" style="minWidth: 150px">
                        <div class="text--surround">{{ data.PropertyCode }}</div>
                    </div>
                    <div class="text-align-left cell--item" style="minWidth: 300px">
                        <div class="text--surround">{{ data.PropertyName }}</div>
                    </div>
                    <div class="text-align-left cell--item" style="minWidth: 250px">
                        <div class="text--surround">{{ data.PropertyTypeName }}</div>
                    </div>
                    <div class="text-align-left cell--item" style="minWidth: 250px">
                        <div class="text--surround">{{ data.DepartmentName }}</div>
                    </div>
                    <div class="text-align-right cell--item" style="minWidth: 100px">
                        <div class="text--surround">{{ this.formatedMoney(data.Quantity) }}</div>
                    </div>
                    <div class="text-align-right cell--item" style="minWidth: 150px">
                        <div class="text--surround">{{ this.formatedMoney(data.OriginalPrice) }}</div>
                    </div>
                    <div class="text-align-right cell--item" style="minWidth: 150px">
                        <div class="text--surround">{{ this.formatedMoney(data.WearRateValue) }}</div>
                    </div>
                    <div class="text-align-right cell--item" style="minWidth: 150px">
                        <div class="text--surround">
                            {{ this.formatedMoney(data.OriginalPrice - data.WearRateValue) }}
                        </div>
                    </div>
                    <div class="cell--item" style="minWidth: 120px"></div>
                </div>
            </div>
            <div style="width: 1666px" class="table__content--sumary" v-if="this.$store.getters.getIsShowSummary">
                <div class="text-align-center cell--item" style="minWidth: 50px"></div>
                <div style="minWidth: 50px"></div>
                <div style="minWidth: 150px"></div>
                <div style="minWidth: 300px"></div>
                <div style="minWidth: 250px"></div>
                <div style="minWidth: 250px"></div>
                <div style="minWidth: 100px; font-weight: bold; padding: 0px 16px" class="text-align-right">
                    {{ this.formatedMoney(this.totalSummary.TotalQuantity) }}
                </div>
                <div style="minWidth: 150px; font-weight: bold; padding: 0px 16px" class="text-align-right">
                    {{ this.formatedMoney(this.totalSummary.TotalOriginalPrice) }}
                </div>
                <div style="minWidth: 150px; font-weight: bold; padding: 0px 16px" class="text-align-right">
                    {{ this.formatedMoney(this.totalSummary.TotalWearRateValue) }}
                </div>
                <div style="minWidth: 150px; font-weight: bold; padding: 0px 16px" class="text-align-right">
                    {{
                        this.formatedMoney(this.totalSummary.TotalOriginalPrice - this.totalSummary.TotalWearRateValue)
                    }}
                </div>
                <div style="minWidth: 120px"></div>
            </div>
        </div>
        <div class="table__fixed" :class="{ 'table__fixed--summary': !this.$store.getters.getIsShowPaging }">
            <div class="table__fixed--header">Chức năng</div>
            <div class="table__fixed--body" ref="fixedBody" @scroll="scrollHandler($event)">
                <div
                    v-for="(data, index) in this.dataRender"
                    :key="index"
                    :class="{
                        'row-selected': isSelected(data.PropertyId),
                        'row-hovered': this.hoveredRowIndex == data.PropertyId || this.indexContextMenu == index,
                    }"
                    class="table-list-icons cell--item"
                    @mouseover="handleMouseOver(data.PropertyId)"
                    @mouseout="handleMouseOut"
                    @click="clickOnRowTable(index, data.PropertyId, $event)"
                >
                    <div
                        v-tippy="$_MISAResource['vn-VI'].edit"
                        class="table--icon table--icon-pencil"
                        @click="this.showDetail(index, data.id)"
                    ></div>
                    <div
                        v-tippy="$_MISAResource['vn-VI'].duplicate"
                        @click.stop
                        class="table--icon table--icon-duplicate"
                        @click="this.showDuplicate(index, data.id)"
                    ></div>
                </div>
            </div>
            <div class="table__fixed--sumary" v-if="this.$store.getters.getIsShowSummary"></div>
        </div>
        <div class="table__paging paging" v-if="this.$store.getters.getIsShowPaging">
            <m-pagination
                :dataSelect="this.numberOfRecordsPerPage"
                :numberPages="this.pageNumber * 10"
                @changeCurrentPage="changeCurrentPage"
                :pageSize="this.pageSize"
                :totalRecords="this.totalRecords"
                @changePageSize="changePageSize"
            ></m-pagination>
        </div>
        <div class="table__content--empty" v-if="isShowEmptyRecord">
            <div class="icon--empty"></div>
        </div>
        <div v-if="this.isLoadingData" class="grid-loading-container">
            <div class="ld-row m-row"><div class="flex ld-item shimmer"></div></div>
            <div class="ld-row m-row"><div class="flex ld-item shimmer"></div></div>
            <div class="ld-row m-row"><div class="flex ld-item shimmer"></div></div>
            <div class="ld-row m-row"><div class="flex ld-item shimmer"></div></div>
            <div class="ld-row m-row"><div class="flex ld-item shimmer"></div></div>
            <div class="ld-row m-row"><div class="flex ld-item shimmer"></div></div>
            <div class="ld-row m-row"><div class="flex ld-item shimmer"></div></div>
            <div class="ld-row m-row"><div class="flex ld-item shimmer"></div></div>
            <div class="ld-row m-row"><div class="flex ld-item shimmer"></div></div>
            <div class="ld-row m-row"><div class="flex ld-item shimmer"></div></div>
            <div class="ld-row m-row"><div class="flex ld-item shimmer"></div></div>
            <div class="ld-row m-row"><div class="flex ld-item shimmer"></div></div>
            <div class="ld-row m-row"><div class="flex ld-item shimmer"></div></div>
            <div class="ld-row m-row"><div class="flex ld-item shimmer"></div></div>
            <div class="ld-row m-row"><div class="flex ld-item shimmer"></div></div>
            <div class="ld-row m-row"><div class="flex ld-item shimmer"></div></div>
        </div>
    </div>
    <PropertyAdd
        ref="propertyForm"
        v-if="isShowAddProperty"
        @addNewProperty="addNewProperty"
        @updateValueRow="updateValueRowSelected"
        @hideAddProperty="hideAddProperty"
        @resetSelectedRow="this.resetSelectedRow"
        @showDialog="this.showDialog"
        @closeDialogAndForm="this.closeDialogAndForm"
        @closeDialog="this.closeDialog"
        :selectedRow="this.selectedData"
        :propertyCodeSample="this.propertyCodeSample"
        :departmentCodes="this.departmentCodes"
        :propertyTypeCodes="this.propertyTypeCodes"
        :formMode="this.formMode"
    ></PropertyAdd>
    <m-modal v-if="this.isShowModal">
        <m-dialog
            :type="this.typeDialog"
            :text="this.textDialog"
            :status="this.statusDialog"
            :documentInfo="this.documentInfoDialog"
            :firstBtnFunction="this.firstBtnFunction"
            :secondBtnFunction="this.secondBtnFunction"
            :thirdBtnFunction="this.thirdBtnFunction"
            :firstBtnLabel="this.firstDialogBtnText"
            :secondBtnLabel="this.secondDialogBtnText"
            :thirdBtnLabel="this.thirdDialogBtnText"
            :dialogActions="this.dialogActions"
        ></m-dialog>
    </m-modal>

    <m-context-menu
        @showDetail="this.showDetail(this.indexSelected, this.idSelected)"
        @showDuplicate="this.showDuplicate(this.indexSelected, this.idSelected)"
        @deleteOneRow="this.deleteOneRow(this.idSelected, this.indexSelected)"
        :position="this.contextMenuPosition"
        :isShowContext="this.$store.getters.getIsShowContextMenu"
    ></m-context-menu>
    <m-toast :label="this.labelToastSuccess" icon="icon--success" v-if="isShowToastSuccess"></m-toast>
    <m-toast :label="this.labelToastError" icon="icon--error" v-if="isShowToastError"></m-toast>
</template>

<script scoped>
import { formatMoney } from '@/common/common';
import { delay } from '@/common/common';
import PropertyAdd from './PropertyAdd.vue';
import instance from '@/common/instance';
import request from '@/common/api';
import debounce from 'lodash/debounce';
import { directive } from 'vue-tippy';
import exception from '@/common/exception';

export default {
    name: 'EstateList',
    components: {
        PropertyAdd,
    },
    directives: {
        tippy: directive,
    },
    data() {
        return {
            // Biến đánh dấu show summary
            isShowSummary: true,
            // Biến đánh dấu show paging
            isShowPaging: true,
            // Biến đánh dấu show setting
            isShowMiniSetting: false,
            // Function của button đầu tiên từ trái sang trong dialog
            firstBtnFunction: null,
            // Function của button thứ 2 từ trái sang trong dialog
            secondBtnFunction: null,
            // Function của button thứ 3 từ trái sang trong dialog
            thirdBtnFunction: null,
            // Nhãn của các button trong dialog
            statusDialog: -1,
            typeDialog: '',
            firstDialogBtnText: '',
            secondDialogBtnText: '',
            thirdDialogBtnText: '',
            beginText: '',
            textDialog: '',
            endText: '',
            // Biến đánh dấu show modal
            isShowModal: false,
            isShowModalRespone: false,
            // Biến đánh dấu show context menu hay không
            isShowContext: false,
            // Vị trí của hàng được show context menu
            indexContextMenu: null,
            // Mã code tự động sinh được trả về từ backend
            propertyCodeSample: null,
            // index của hàng được chọn để lên form
            indexSelected: null,
            // id của hàng được chọn để lên form
            idSelected: null,
            // Chiều dài và rộng của context menu
            contextMenuPosition: {},
            // Biến đánh dấu là xóa nhiều hay không
            isMultipleDelele: false,
            // Biến lưu trạng thái form
            formMode: null,
            // Lưu danh sách header
            listHeader: [],
            // Giá trị ô lọc mã tài sản
            propertyTypeFilter: '',
            // Giá trị ô lọc tên phòng ban sử dụng
            departmentUseFilter: '',
            // Danh sách bộ phận sử dụng
            Departments: [],
            // Danh sách loại tài sản
            PropertyTypes: [],
            // Giá trị ô tìm kiếm
            searchInput: '',
            // lưu các mã phòng ban để đẩy vào cho component combobox
            departmentCodes: [],
            // lưu các mã loại tài sản để đẩy vào cho component combobox
            propertyTypeCodes: [],
            // Biến check show form detail hay không
            isShowDetail: false,
            // Biến check show toast success hay không
            isShowToastSuccess: false,
            // Biến check show toast error hay không
            isShowToastError: false,
            // Nội dung của toast success
            labelToastSuccess: '',
            // Nội dung của toast error
            labelToastError: '',
            // Biến check show form thêm tài sản hay không
            isShowAddProperty: false,
            // Lưu giá trị hàng được chọn hiển thị lên form thêm hoặc nhân bản
            selectedData: null,
            // Lưu danh sách id của các hàng được chọn
            selectedRow: [],
            // Mặc định số lượng bản ghi trên trang
            pageSize: '20',
            // Tổng số bản ghi
            totalRecords: 0,
            // Mảng lưu giá trị các cột trong summary
            totalSummary: {},
            // Mặc định giá trị page hiện tại
            currentPage: 1,
            // Số lượng trang
            pageNumber: 0,
            // Lưu trạng thái loading
            isLoadingData: false,
            // Lưu giá trị các bản ghi hiển thị lên table
            dataTable: [],
            // Lưu giá trị các bản ghi hiển thị lên table
            dataRender: [],
            // Lưu giá trị index của hàng được hover
            hoveredRowIndex: -1,
            // Lưu trạng thái hiển thị không có bản ghi hay không
            isShowEmptyRecord: false,
            numberOfRecordsPerPage: [
                {
                    label: '20',
                    value: '20',
                },
                {
                    label: '40',
                    value: '40',
                },
                {
                    label: '60',
                    value: '60',
                },
            ],
            // Đánh dấu hàng đầu tiên được chọn khi dùng sự kiện shift + click
            firstClickRow: 0,
            // Đánh dấu hàng thứ hai được chọn khi dùng sự kiện shift + click
            secondClickRow: '',
            errorFieldName: '',
            documentInfoDialog: [],
            dialogActions: {
                firstDialogBtnText: '',
                secondDialogBtnText: '',
                thirdDialogBtnText: '',
                firstBtnFunction: null,
                secondBtnFunction: null,
                thirdBtnFunction: null,
            },
        };
    },
    async created() {
        // Lấy ra list header
        this.listHeader = this.$_MISAResource['vn-VI'].listHeader;
        // Lấy ra danh sách phòng ban để đưa vào combo box
        this.getAllDepartments();
        // Lấy ra danh sách mã loại tài sản để đưa vào combo box
        this.getAllPropertyTypes();
        // Lấy dữ liệu phân trang
        await this.getPropertyWithFilter();
        // Tính toán số trang
        this.calculateNumberPage();
    },
    watch: {
        pageSize: function (newValue) {
            this.pageSize = newValue;
            this.currentPage = 1;
            this.calculateNumberPage();
            this.getPropertyWithFilter();

            // Reset hết các giá trị đang được checked
            for (let i = 0; i < this.selectedRow.length; i++) {
                this.$refs[`checkbox-${this.selectedRow[i]}`][0].isChecked = false;
            }
            this.$refs['checkbox-all'][0].isChecked = false;
            this.selectedRow = [];

            for (let i = 0; i < this.selectedRow.length; i++) {
                this.$refs[`checkbox-${this.selectedRow[i]}`][0].isChecked = true;
            }
            this.checkFullChecked();
        },
        currentPage: async function () {
            await this.getPropertyWithFilter();

            for (let i = 0; i < this.dataRender.length; i++) {
                if (
                    this.$refs[`checkbox-${this.dataRender[i].PropertyId}`] &&
                    this.$refs[`checkbox-${this.dataRender[i].PropertyId}`][0]
                ) {
                    this.$refs[`checkbox-${this.dataRender[i].PropertyId}`][0].isChecked = false;
                }
            }
            for (let i = 0; i < this.selectedRow.length; i++) {
                if (this.$refs[`checkbox-${this.selectedRow[i]}`] && this.$refs[`checkbox-${this.selectedRow[i]}`][0]) {
                    this.$refs[`checkbox-${this.selectedRow[i]}`][0].isChecked = true;
                }
            }

            this.checkFullChecked();
        },
        propertyTypeFilter: async function () {
            await this.getPropertyWithFilter();

            this.calculateNumberPage();

            for (let i = 0; i < this.dataRender.length; i++) {
                if (
                    this.$refs[`checkbox-${this.dataRender[i].PropertyId}`] &&
                    this.$refs[`checkbox-${this.dataRender[i].PropertyId}`][0]
                ) {
                    this.$refs[`checkbox-${this.dataRender[i].PropertyId}`][0].isChecked = false;
                }
            }

            this.selectedRow = [];

            this.checkFullChecked();
        },
        departmentUseFilter: async function () {
            await this.getPropertyWithFilter();

            this.calculateNumberPage();

            for (let i = 0; i < this.dataRender.length; i++) {
                if (
                    this.$refs[`checkbox-${this.dataRender[i].PropertyId}`] &&
                    this.$refs[`checkbox-${this.dataRender[i].PropertyId}`][0]
                ) {
                    this.$refs[`checkbox-${this.dataRender[i].PropertyId}`][0].isChecked = false;
                }
            }

            this.selectedRow = [];
        },
        searchInput: debounce(async function () {
            await this.getPropertyWithFilter();
            this.calculateNumberPage();

            for (let i = 0; i < this.dataRender.length; i++) {
                if (
                    this.$refs[`checkbox-${this.dataRender[i].PropertyId}`] &&
                    this.$refs[`checkbox-${this.dataRender[i].PropertyId}`][0]
                ) {
                    this.$refs[`checkbox-${this.dataRender[i].PropertyId}`][0].isChecked = false;
                }
            }

            this.selectedRow = [];
        }, 1000),

        selectedRow: function () {
            for (let i = 0; i < this.dataRender.length; i++) {
                if (
                    this.$refs[`checkbox-${this.dataRender[i].PropertyId}`] &&
                    this.$refs[`checkbox-${this.dataRender[i].PropertyId}`][0]
                ) {
                    this.$refs[`checkbox-${this.dataRender[i].PropertyId}`][0].isChecked = false;
                }
            }
            for (let i = 0; i < this.selectedRow.length; i++) {
                if (this.$refs[`checkbox-${this.selectedRow[i]}`] && this.$refs[`checkbox-${this.selectedRow[i]}`][0]) {
                    this.$refs[`checkbox-${this.selectedRow[i]}`][0].isChecked = true;
                }
            }

            this.checkFullChecked();
        },
    },
    methods: {
        changePageSize: function (newValue) {
            this.pageSize = newValue;
        },

        /*
         * Lấy dữ liệu phòng ban để đưa vào combobox
         * Author: BATUAN (14/06/2023)
         */
        async getAllDepartments() {
            await request
                .getRecord('Department')
                .then((response) => {
                    this.Departments = response.data;
                })
                .catch((err) => {
                    this.handleException(err.statusCode, err.message, err.documentInfo, this.showDialog);
                });

            this.Departments.forEach((department) => {
                this.departmentCodes.push({
                    id: department.DepartmentId,
                    value: department.DepartmentName,
                    label: department.DepartmentCode,
                });
            });
        },
        /*
         * Lấy dữ liệu loại tài sản để đưa vào combobox
         * Author: BATUAN (14/06/2023)
         */
        async getAllPropertyTypes() {
            await request
                .getRecord('PropertyType')
                .then((response) => {
                    this.PropertyTypes = response.data;
                })
                .catch((err) => {
                    this.handleException(err.statusCode, err.message, err.documentInfo, this.showDialog);
                });

            this.PropertyTypes.forEach((propertyType) => {
                this.propertyTypeCodes.push({
                    id: propertyType.PropertyTypeId,
                    value: propertyType.PropertyTypeName,
                    label: propertyType.PropertyTypeCode,
                    wearRate: propertyType.WearRate,
                    numberYearUse: propertyType.NumberYearUse,
                });
            });
        },
        /*
         * Lấy dữ liệu của 1 trang có lọc dữ liệu theo combobox
         * Author: BATUAN (08/06/2023)
         */
        async getPropertyWithFilter() {
            this.isLoadingData = true;
            await request
                .getRecord(
                    `Property/filter?pageNumber=${this.currentPage}&pageSize=${Number(this.pageSize)}&searchInput=${
                        this.searchInput
                    }&propertyType=${this.propertyTypeFilter}&departmentName=${this.departmentUseFilter}`,
                )
                .then((response) => {
                    // lưu dữ liệu data hiển thị
                    this.dataRender = response.data.Data;
                    // lưu tổng số bản ghi
                    this.totalRecords = response.data.Total[0].TotalRecord;
                    // lưu giá trị tổng số
                    this.totalSummary = response.data.Total[0];
                    // lưu dữ liệu vào mảng phục vụ việc truy xuất khi sang trang khác(xóa nhiều)
                    this.dataRender.forEach((data) => {
                        this.dataTable.push(data);
                    });
                })
                .catch((err) => {
                    this.handleException(err.statusCode, err.message, err.documentInfo, this.showDialog);
                });

            setTimeout(() => {
                this.isLoadingData = false;
            }, 500);
            // kiểm tra xem mảng dataRender có rỗng hay không
            if (this.dataRender.length === 0) {
                this.isShowEmptyRecord = true;
            } else {
                this.isShowEmptyRecord = false;
            }
        },
        /*
         * Tạo mã code mới khi mở form thêm
         * Author: BATUAN (16/06/2023)
         */
        async generateSampleCode() {
            this.$store.commit('toggleMaskElementShow');
            await instance
                .get('Property/GetLastestCode')
                .then((res) => {
                    this.propertyCodeSample = res.data;
                })
                .catch((err) => {
                    this.handleException(err.statusCode, err.message, this.showDialog);
                });

            await delay(300);
            this.$store.commit('toggleMaskElementShow');
        },
        /*
         * Lấy dữ liệu tất cả bản ghi trong db
         * Author: BATUAN (08/06/2023)
         */
        async getAllProperty() {
            await request
                .getRecord('Property')
                .then((response) => {
                    this.dataTable = response.data;
                })
                .catch((err) => {
                    this.handleException(err.statusCode, err.message, this.showDialog);
                });
        },
        /*
         * Tính toán số trang
         * Author: BATUAN (08/06/2023)
         */
        calculateNumberPage() {
            this.pageNumber = Math.ceil(this.totalRecords / this.pageSize);
        },
        /*
         * Hiển thị trang chi tiết và gán các giá trị của hàng được chọn
         * Author: BATUAN (27/05/2023)
         */
        async showDetail(index, id) {
            this.$store.commit('toggleMaskElementShow');
            await delay(300);
            this.$store.commit('toggleMaskElementShow');
            // Gán form mode thành form update
            this.formMode = this.$_MISAEnum.formUpdate;
            // Hiển thị màn hình update(giống thêm mới)
            this.isShowAddProperty = true;
            // Lấy giá trị data để hiển thị lên màn chi tiết
            this.selectedData = this.dataRender[index];
            // Lưu index hàng được chọn
            this.indexSelected = index;
            // Lưu id hàng được chọn
            this.idSelected = id;
        },
        /*
         * Hiển thị trang chi tiết(nhân bản)
         * Author: BATUAN (27/06/2023)
         */
        async showDuplicate(index, id) {
            // Lấy mã code tự sinh
            await this.generateSampleCode();
            // Gán giá trị form mode thành form nhân bản
            this.formMode = this.$_MISAEnum.formDuplicate;
            // Hiển thị màn hình thêm mới
            this.isShowAddProperty = true;
            // Lấy giá trị data
            this.selectedData = this.dataRender[index];
            // Lưu index hàng được chọn
            this.indexSelected = index;
            // Lưu id hàng được chọn
            this.idSelected = id;
        },
        /*
         * Ẩn trang chi tiết
         * Author: BATUAN (27/05/2023)
         */
        closeDetail() {
            this.isShowDetail = false;
        },
        /*
         * Show toast hiển thị thành công
         * Author: BATUAN (27/05/2023)
         */
        showToastSuccess(label) {
            this.isShowToastSuccess = true;
            this.labelToastSuccess = label;
            // Ẩn sau 3s
            setTimeout(() => {
                this.isShowToastSuccess = false;
            }, 3000);
        },
        /*
         * Show toast hiển thị hành động thất bại
         * Author: BATUAN (27/05/2023)
         */
        showToastError(label) {
            this.isShowToastError = true;
            this.labelToastError = label;
            // Ẩn sau 3s
            setTimeout(() => {
                this.isShowToastError = false;
            }, 3000);
        },
        /*
         * Hiển thị trang thêm tài sản
         * Author: BATUAN (27/05/2023)
         */
        async showAddProperty() {
            await this.generateSampleCode();
            this.formMode = this.$_MISAEnum.formAdd;
            this.isShowAddProperty = true;
        },
        /*
         * Ẩn trang thêm tài sản
         * Author: BATUAN (27/05/2023)
         */
        hideAddProperty() {
            this.isShowAddProperty = false;
        },
        /*
         * Update dữ liệu khi lưu dữ liệu trong form chỉnh sửa
         * Author: BATUAN (27/05/2023)
         */
        async updateValueRowSelected(newValue) {
            newValue.OriginalPrice = Number(newValue.OriginalPrice);
            newValue.WearRateValue = Number(newValue.WearRateValue);
            newValue.Quantity = Number(newValue.Quantity);
            newValue.NumberYearUse = Number(newValue.NumberYearUse);
            newValue.WearRate = Number(newValue.WearRate);

            let listUpdate = [];
            listUpdate.push(newValue);
            let isSuccess = false;
            this.$store.commit('toggleMaskElementShow');
            // update giá trị trên db
            await request
                .updateRecord('Property', listUpdate)
                .then(() => {
                    isSuccess = true;
                })
                .catch((err) => {
                    this.$store.commit('toggleMaskElementShow');
                    this.handleException(err.statusCode, err.message, this.showDialog);
                });

            if (isSuccess) {
                await delay(300);
                this.$store.commit('toggleMaskElementShow');
                // Ẩn form
                this.hideAddProperty();
                // Show toast success
                this.showToastSuccess(this.$_MISAResource['vn-VI'].updatePropertySuccess);
                // Lấy lại danh sách hiển thị trên table
                this.getPropertyWithFilter();
            }
        },
        /*
         * Sự kiện click vào hàng của table
         * Author: BATUAN (12/06/2023)
         */
        clickOnRowTable(index, id, event) {
            // Nếu là sự kiện double click thì bỏ qua
            if (event.detail == 2) {
                return;
            }
            // Sự kiện Ctrl + Click
            if (event.ctrlKey) {
                if (!this.selectedRow.includes(id)) {
                    this.selectedRow.push(id);

                    for (let i = 0; i < this.dataRender.length; i++) {
                        if (
                            this.$refs[`checkbox-${this.dataRender[i].PropertyId}`] &&
                            this.$refs[`checkbox-${this.dataRender[i].PropertyId}`][0]
                        ) {
                            this.$refs[`checkbox-${this.dataRender[i].PropertyId}`][0].isChecked = false;
                        }
                    }
                    for (let i = 0; i < this.selectedRow.length; i++) {
                        if (
                            this.$refs[`checkbox-${this.selectedRow[i]}`] &&
                            this.$refs[`checkbox-${this.selectedRow[i]}`][0]
                        ) {
                            this.$refs[`checkbox-${this.selectedRow[i]}`][0].isChecked = true;
                        }
                    }

                    this.checkFullChecked();
                } else {
                    this.selectedRow = this.selectedRow.filter((num) => {
                        return num != id;
                    });
                }
            } else if (event.shiftKey) {
                if (this.firstClickRow || this.firstClickRow === 0) {
                    this.secondClickRow = index;
                    if (this.secondClickRow > this.firstClickRow) {
                        this.selectedRow = this.selectedRow.filter((id) => {
                            return this.dataRender.some((item) => item.id === id);
                        });
                        for (let i = this.firstClickRow; i <= this.secondClickRow; i++) {
                            const propertyId = this.dataRender[i].PropertyId;
                            if (!this.selectedRow.includes(propertyId)) {
                                this.selectedRow.push(propertyId);
                            }

                            for (let i = 0; i < this.dataRender.length; i++) {
                                if (
                                    this.$refs[`checkbox-${this.dataRender[i].PropertyId}`] &&
                                    this.$refs[`checkbox-${this.dataRender[i].PropertyId}`][0]
                                ) {
                                    this.$refs[`checkbox-${this.dataRender[i].PropertyId}`][0].isChecked = false;
                                }
                            }
                            for (let i = 0; i < this.selectedRow.length; i++) {
                                if (
                                    this.$refs[`checkbox-${this.selectedRow[i]}`] &&
                                    this.$refs[`checkbox-${this.selectedRow[i]}`][0]
                                ) {
                                    this.$refs[`checkbox-${this.selectedRow[i]}`][0].isChecked = true;
                                }
                            }
                            this.checkFullChecked();
                        }
                    } else {
                        this.selectedRow = this.selectedRow.filter((id) => {
                            return this.dataRender.some((item) => item.id === id);
                        });
                        for (let i = this.secondClickRow; i <= this.firstClickRow; i++) {
                            const propertyId = this.dataRender[i].PropertyId;
                            if (!this.selectedRow.includes(propertyId)) {
                                this.selectedRow.push(propertyId);
                            }

                            for (let i = 0; i < this.dataRender.length; i++) {
                                if (
                                    this.$refs[`checkbox-${this.dataRender[i].PropertyId}`] &&
                                    this.$refs[`checkbox-${this.dataRender[i].PropertyId}`][0]
                                ) {
                                    this.$refs[`checkbox-${this.dataRender[i].PropertyId}`][0].isChecked = false;
                                }
                            }
                            for (let i = 0; i < this.selectedRow.length; i++) {
                                if (
                                    this.$refs[`checkbox-${this.selectedRow[i]}`] &&
                                    this.$refs[`checkbox-${this.selectedRow[i]}`][0]
                                ) {
                                    this.$refs[`checkbox-${this.selectedRow[i]}`][0].isChecked = true;
                                }
                            }

                            this.checkFullChecked();
                        }
                    }
                }
            } else {
                this.handleClickOnRow(index, id);
            }
        },
        /*
         * Thêm một bản ghi mới vào dữ liệu
         * Author: BATUAN (14/06/2023)
         */
        async addNewProperty(newProperty) {
            // chuyển đổi các trường sang dạng số
            newProperty.OriginalPrice = Number(newProperty.OriginalPrice);
            newProperty.WearRateValue = Number(newProperty.WearRateValue);
            newProperty.Quantity = Number(newProperty.Quantity);
            newProperty.NumberYearUse = Number(newProperty.NumberYearUse);
            newProperty.WearRate = Number(newProperty.WearRate);
            //biến cờ đánh dấu chưa thành công
            let listNewProperty = [];
            listNewProperty.push(newProperty);
            let isSuccess = false;
            this.$store.commit('toggleMaskElementShow');
            await request
                .insertRecord('Property', listNewProperty)
                .then(() => (isSuccess = true))
                .catch(async (err) => {
                    this.$store.commit('toggleMaskElementShow');
                    this.handleException(err.statusCode, err.message, err.documentInfo, this.showDialog);
                    this.errorFieldName = err.errorField;
                });

            if (isSuccess) {
                this.hideAddProperty();
                await delay(300);
                this.$store.commit('toggleMaskElementShow');
                this.showToastSuccess(this.$_MISAResource['vn-VI'].addPropertySuccess);
                await this.getPropertyWithFilter();
                this.checkForCheckbox();
                this.calculateNumberPage();
            }
        },
        /*
         * Format giá trị tiền
         * Author: BATUAN (27/05/2023)
         */
        formatedMoney(value) {
            return formatMoney(value);
        },
        /*
         * Reset các giá trị của các hàng đã chọn
         * Author: BATUAN (14/06/2023)
         */
        resetSelectedRow() {
            this.selectedData = null;
        },
        /*
         * Sự kiện khi ô checkbox được check
         * Author: BATUAN (08/06/2023)
         */
        checkedCheckbox(id) {
            this.selectedRow.push(id);

            for (let i = 0; i < this.dataRender.length; i++) {
                if (
                    this.$refs[`checkbox-${this.dataRender[i].PropertyId}`] &&
                    this.$refs[`checkbox-${this.dataRender[i].PropertyId}`][0]
                ) {
                    this.$refs[`checkbox-${this.dataRender[i].PropertyId}`][0].isChecked = false;
                }
            }
            for (let i = 0; i < this.selectedRow.length; i++) {
                if (this.$refs[`checkbox-${this.selectedRow[i]}`] && this.$refs[`checkbox-${this.selectedRow[i]}`][0]) {
                    this.$refs[`checkbox-${this.selectedRow[i]}`][0].isChecked = true;
                }
            }

            this.checkFullChecked();
        },
        /*
         * Kiểm tra xem tất cả các hàng trong 1 trang table có được chọn hay không
         * Author: BATUAN (28/06/2023)
         */
        checkFullChecked() {
            let check = true;
            this.dataRender.forEach((data) => {
                if (!this.selectedRow.includes(data.PropertyId)) {
                    check = false;
                }
            });

            if (check == true && this.dataRender.length > 0) {
                this.$refs['checkbox-all'][0].isChecked = true;
            } else {
                this.$refs['checkbox-all'][0].isChecked = false;
            }
        },
        /*
         * Sự kiện khi ô checkbox uncheck
         * Author: BATUAN (08/06/2023)
         */
        uncheckedCheckbox(id) {
            this.selectedRow = this.selectedRow.filter((item) => item !== id);
        },
        /*
         * Check xem 1 hàng có đang được chọn hay không
         * Author: BATUAN (14/06/2023)
         */
        isSelected(id) {
            if (this.selectedRow) {
                return this.selectedRow.includes(id);
            }
            return false;
        },
        /*
         * Sự kiện khi hover vào 1 hàng
         * Author: BATUAN (14/06/2023)
         */
        handleMouseOver(id) {
            setTimeout(() => {
                this.hoveredRowIndex = id;
            }, 54);
        },
        /*
         * Sự kiện khi thoát hover khỏi 1 hàng
         * Author: BATUAN (14/06/2023)
         */
        handleMouseOut() {
            this.hoveredRowIndex = -1;
        },
        /*
         * Chọn tất cả các row
         * Author: BATUAN (08/06/2023)
         */
        selectAllRow() {
            let targetRef = [];
            if (this.dataRender) {
                for (let i = 0; i < this.dataRender.length; i++) {
                    if (!this.selectedRow.includes(this.dataRender[i].PropertyId)) {
                        this.selectedRow.push(this.dataRender[i].PropertyId);
                        targetRef.push(`checkbox-${this.dataRender[i].PropertyId}`);
                    }
                }
            }
            targetRef.forEach((targetRef) => {
                this.$refs[targetRef][0].isChecked = true;
            });
        },
        /*
         * Bỏ chọn tất cả các hàng
         * Author: BATUAN (08/06/2023)
         */
        unSelectAllRow() {
            this.selectedRow = this.selectedRow.filter(
                (item) => !this.dataRender.some((obj) => obj.PropertyId === item),
            );
        },
        /*
         * Sự kiện khi click vào biểu tượng xóa nhiều
         * Author: BATUAN (08/06/2023)
         */
        deleteRowOnClickIcon() {
            if (this.selectedRow.length <= 0) {
                this.showDialog(this.$_MISAResource['vn-VI'].chooseAtLeastOne, '', {
                    thirdBtnFunction: this.closeDialog,
                    thirdDialogBtnText: this.$_MISAResource['vn-VI'].close,
                });
            } else {
                if (this.selectedRow.length == 1) {
                    let record = this.dataTable.find((data) => {
                        return data.PropertyId == this.selectedRow[0];
                    });
                    let code = record.PropertyCode;
                    let name = record.PropertyName;

                    this.textDialog = `${this.$_MISAResource['vn-VI'].confirm} <strong>${code}-${name} ? </strong>`;
                } else {
                    let beginText =
                        this.selectedRow.length >= 10 ? this.selectedRow.length : `0${this.selectedRow.length}`;
                    this.textDialog = `<strong>${beginText}</strong> ${this.$_MISAResource['vn-VI'].deleteMultipleMessage}`;
                }
                this.showDialog(this.textDialog, '', {
                    firstBtnFunction: this.closeDialog,
                    firstDialogBtnText: this.$_MISAResource['vn-VI'].cancel,
                    thirdBtnFunction: this.deleteRows,
                    thirdDialogBtnText: this.$_MISAResource['vn-VI'].delete,
                });
            }
            this.isMultipleDelele = true;
        },
        /*
         * Sự kiện khi click vào biểu tượng xóa ở hàng của table
         * Author: BATUAN (08/06/2023)
         */
        deleteOneRow(id,index) {
            let code = this.dataRender[index].PropertyCode;
            let name = this.dataRender[index].PropertyName;

            this.textDialog = `${this.$_MISAResource['vn-VI'].confirm} <strong>${code}-${name}?</strong>`;

            this.showDialog(this.textDialog, [], {
                firstBtnFunction: this.closeDialog,
                firstDialogBtnText: this.$_MISAResource['vn-VI'].cancel,
                thirdBtnFunction: () => this.deleteRows(id,index),
                thirdDialogBtnText: this.$_MISAResource['vn-VI'].delete,
            });

            this.isMultipleDelele = false;
        },
        /*
         * Xóa các hàng được chọn
         * Author: BATUAN (08/06/2023)
         */
        async deleteRows(id, index) {
            if (this.isMultipleDelele) {
                //Xóa các bản ghi
                await instance
                    .delete(`Property`, { data: this.selectedRow })
                    .then(async () => {
                        this.showToastSuccess('Xóa thành công');
                        // Reset list các hàng được chọn && set ô checkbox là unchecked
                        for (let i = 0; i < this.selectedRow.length; i++) {
                            if (
                                this.$refs[`checkbox-${this.selectedRow[i]}`] &&
                                this.$refs[`checkbox-${this.selectedRow[i]}`][0]
                            )
                                this.$refs[`checkbox-${this.selectedRow[i]}`][0].isChecked = false;
                        }
                        this.selectedRow = [];

                        this.closeDialogAndForm();

                        await this.getPropertyWithFilter();

                        this.calculateNumberPage();

                        this.$refs['checkbox-all'][0].isChecked = false;
                    })
                    .catch((err) => {
                        this.handleException(err.statusCode, err.message, err.documentInfo, this.showDialog);
                    });
            }
            // Xóa 1 bản ghi
            else {
                console.log(index)
                let row = [];
                row.push(id)
                await instance
                    .delete(`Property`, { data: row })
                    .then(async () => {
                        // Hiện toast message thông báo thành công
                        this.showToastSuccess('Xóa thành công');
                        // Xóa hàng bị xóa khỏi danh sách các hàng đang được chọn (nếu có)
                        this.selectedRow = this.selectedRow.filter((rowId) => {
                            return id != rowId;
                        });
                        if (this.$refs[`checkbox-${id}`] && this.$refs[`checkbox-${id}`][0]) {
                            this.$refs[`checkbox-${id}`][0].isChecked = false;
                        }
                        this.closeDialogAndForm();

                        await this.getPropertyWithFilter();

                        this.calculateNumberPage();

                        this.$refs['checkbox-all'][0].isChecked = false;
                    })
                    .catch((err) => {
                        console.log(err);
                        this.handleException(err.statusCode, err.message, err.documentInfo, this.showDialog);
                    });
            }
        },
        /*
         * Sự kiện khi thay đổi giá trị của currentPage khi click sang trái hoặc phải ở phần paging
         * Author: BATUAN (07/06/2023)
         */
        changeCurrentPage(newPage) {
            this.currentPage = newPage;
        },
        /*
         * Sự kiện khi click vào ô checkbox
         * Author: BATUAN (07/06/2023)
         */
        clickOnCheckbox(index, id) {
            console.log(index);
            if (this.$refs[`checkbox-${id}`][0].isChecked) {
                this.selectedRow = this.selectedRow.filter((idRow) => {
                    return idRow != id;
                });
            } else {
                this.selectedRow.push(id);
            }
            this.checkForCheckbox();
            this.checkFullChecked();
        },
        /*
         * Sự kiện khi click vào ô checkbox all
         * Author: BATUAN (07/06/2023)
         */
        clickOnCheckBoxAll() {
            if (this.$refs['checkbox-all'][0].isChecked) {
                let propertyListId = [];
                this.dataRender.forEach((element) => {
                    propertyListId.push(element.PropertyId);
                });

                this.selectedRow = this.selectedRow.filter((id) => {
                    return !propertyListId.includes(id);
                });
            } else if (!this.$refs['checkbox-all'][0].isChecked) {
                for (let i = 0; i < this.dataRender.length; i++) {
                    if (!this.selectedRow.includes(this.dataRender[i].PropertyId)) {
                        this.selectedRow.push(this.dataRender[i].PropertyId);
                    }
                }
            }
            this.checkForCheckbox();

            this.checkFullChecked();
        },

        /*
         * Sự kiện khi scroll trong body của table
         * Author: BATUAN (14/06/2023)
         */
        scrollHandler: function (event) {
            const fixedBody = this.$refs.fixedBody;
            const contentBody = this.$refs.contentBody;
            if (event.target == contentBody) {
                fixedBody.scrollTop = contentBody.scrollTop;
            } else {
                contentBody.scrollTop = fixedBody.scrollTop;
            }
        },
        /*
         * Sự kiện khi click vào 1 hàng trong table
         * Author: BATUAN (14/06/2023)
         */
        handleClickOnRow(index, id) {
            // cập nhật lại con trỏ trỏ tới vị trí đầu tiên khi dùng Shift + Click
            this.firstClickRow = index;

            let isMultipleSelect = false;
            let count = 0;
            for (let i = 0; i < this.dataRender.length; i++) {
                if (this.selectedRow.includes(this.dataRender[i].PropertyId)) {
                    count++;
                    if (count >= 2) {
                        isMultipleSelect = true;
                        break;
                    }
                }
            }

            if (!isMultipleSelect && this.selectedRow.includes(id)) {
                this.selectedRow = this.selectedRow.filter(
                    (item) => !this.dataRender.some((obj) => obj.PropertyId === item),
                );
            } else {
                this.selectedRow = this.selectedRow.filter(
                    (item) => !this.dataRender.some((obj) => obj.PropertyId === item),
                );

                this.selectedRow.push(id);
            }
        },
        /*
         * Phương thức liên quan đến sự kiện check cho các ô check box khi các hàng được chọn thay đổi
         * Author: BATUAN (29/06/2023)
         */
        checkForCheckbox() {
            for (let i = 0; i < this.dataRender.length; i++) {
                if (
                    this.$refs[`checkbox-${this.dataRender[i].PropertyId}`] &&
                    this.$refs[`checkbox-${this.dataRender[i].PropertyId}`][0]
                ) {
                    this.$refs[`checkbox-${this.dataRender[i].PropertyId}`][0].isChecked = false;
                }
            }
            for (let i = 0; i < this.selectedRow.length; i++) {
                if (this.$refs[`checkbox-${this.selectedRow[i]}`] && this.$refs[`checkbox-${this.selectedRow[i]}`][0]) {
                    this.$refs[`checkbox-${this.selectedRow[i]}`][0].isChecked = true;
                }
            }

            this.checkFullChecked();
        },

        /*
         * Show context menu
         * Author: BATUAN (29/06/2023)
         */
        showContextMenu(event, index, id) {
            this.indexSelected = index;
            this.idSelected = id;
            this.indexContextMenu = index;

            const browserHeight = window.innerHeight;
            const browserWidth = window.innerWidth;

            // Tính toán vị trí context menu

            const menuWidth = this.$store.getters.getContextMenuSize.x;
            const menuHeight = this.$store.getters.getContextMenuSize.y;
            let positionX = event.clientX;
            let positionY = event.clientY;

            // Kiểm tra xem context menu có bị che khuất không
            if (positionX + menuWidth > browserWidth) {
                positionX = browserWidth - menuWidth;
            }

            if (positionY + menuHeight > browserHeight) {
                positionY = browserHeight - menuHeight;
            }

            this.contextMenuPosition = { x: positionX, y: positionY };

            this.$store.commit('showContextMenu');

            document.addEventListener('click', this.hideContextMenu);

            document.addEventListener('wheel', this.hideContextMenu);

            event.preventDefault();
        },
        /*
         * Show context menu
         * Author: BATUAN (29/06/2023)
         */
        hideContextMenu() {
            this.$store.commit('hideContextMenu');
            this.indexContextMenu = null;
            document.removeEventListener('click', this.hideContextMenu);
            document.removeEventListener('wheel', this.hideContextMenu);
        },
        /*
         * Xử lý mã lỗi backend trả về
         * Author: BATUAN (29/06/2023)
         */
        handleException(statusCode, message, documentInfo, showDialog) {
            exception(statusCode, message, documentInfo, showDialog);
        },
        /*
         * Sự kiện click chuột phải vào hàng để bôi đậm hàng khi hiện context menu
         * Author: BATUAN (29/06/2023)
         */
        handleRightClick(index) {
            return index;
        },
        /*
         * Hiển thị dialog thông báo
         * Author: BATUAN (29/06/2023)
         */
        showDialog(
            textDialog,
            documentInfo,
            dialogActions = {
                thirdDialogBtnText: 'Đóng',
                thirdBtnFunction: this.closeDialog,
            },
        ) {
            this.isShowModal = true;
            this.documentInfoDialog = documentInfo;
            this.textDialog = textDialog;
            this.dialogActions = dialogActions;
        },
        /*
         * Đóng dialog thông báo xóa
         * Author: BATUAN (08/06/2023)
         */
        closeDialog() {
            this.isShowModal = false;
        },
        closeDialogAndForm() {
            this.isShowModal = false;
            this.isShowDetail = false;
            this.isShowAddProperty = false;
        },
        toggleMiniSetting() {
            this.isShowMiniSetting = !this.isShowMiniSetting;
        },
        /*
         * Sự kiện Ctrl + F để tìm kiếm
         * Author: BATUAN (24/08/2023)
         */
        handleSearchByKeyBoard(event) {
            if (event.key === 'f') {
                {
                    if (event.ctrlKey) {
                        this.$refs.inputSearch.$el.focus();
                        // Ngăn chặn hành vi mặc định của trình duyệt khi nhấn Ctrl + F
                        event.preventDefault();
                    }
                }
            }
        },
    },
};
</script>

<style>
@import url(../../css/elementui/el-select.css);
@import url(../../css/components/tooltip.css);
@import url(../../css/main.css);
.drop-down {
    position: relative;
}
</style>
