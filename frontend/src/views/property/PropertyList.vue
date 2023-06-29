<template>
    <div class="content__navbar">
        <div class="content__navbar-left">
            <div class="text-field">
                <m-text-input
                    v-model="this.searchInput"
                    :placeholder="this.MISAResource['vn-VI'].searchProperty"
                    individualClass="text__input-icon-start"
                ></m-text-input>
            </div>
            <el-select
                v-model="propertyTypeFilter"
                clearable
                :placeholder="this.MISAResource['vn-VI'].propertyType"
                class="back-ground__icon"
                filterable
                :title="propertyTypeFilter"
            >
                <el-option
                    v-for="item in PropertyTypes"
                    :key="item.PropertyTypeId"
                    :label="item.PropertyTypeName"
                    :value="item.PropertyTypeName"
                ></el-option>
            </el-select>
            <el-select
                v-model="departmentUseFilter"
                clearable
                :placeholder="this.MISAResource['vn-VI'].departmentUse"
                class="back-ground__icon"
                filterable
                :title="departmentUseFilter"
            >
                <el-option
                    v-for="item in Departments"
                    :key="item.DepartmentId"
                    :label="item.DepartmentName"
                    :value="item.DepartmentName"
                ></el-option>
            </el-select>
            <m-button
                :label="this.MISAResource['vn-VI'].reload"
                class="content__btn--reload"
                :individualClass="'btn btn--noborder'"
                :icon="'icon--reload'"
                @click="
                    async () => {
                        await this.$store.commit('toggleMaskElement');
                        await this.getPropertyWithFilter();
                    }
                "
            ></m-button>
        </div>
        <div class="content__navbar-right">
            <m-button
                :label="this.MISAResource['vn-VI'].addProperties"
                :individualClass="'btn--primary'"
                @click="this.showAddProperty"
            ></m-button>
            <el-tooltip effect="dark" content="Xuất file excel" placement="bottom-start">
                <m-button :individualClass="' btn--single btn--combo-excel'"></m-button>
            </el-tooltip>
            <el-tooltip effect="dark" content="Xóa" placement="bottom-start">
                <m-button
                    :individualClass="' btn--single'"
                    :icon="'icon--delete'"
                    @click="this.deleteRowOnClickIcon"
                    data-title="Xóa"
                ></m-button>
            </el-tooltip>
        </div>
    </div>
    <div class="content__body">
        <div class="table__content">
            <div class="table__content--header">
                <div
                    class="table__content--header-item cell--item"
                    v-for="(header, index) in this.listHeader"
                    :key="index"
                    :style="{ minWidth: header.width }"
                >
                    <template v-if="header.name === 'checkbox'">
                        <m-checkbox
                            ref="checkbox-all"
                            @selectAllRow="selectAllRow"
                            @unSelectAllRow="unSelectAllRow"
                            type="primary"
                            :class="header.align"
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
            <div class="table__content--body" ref="contentBody" @scroll="scrollHandler($event)">
                <div
                    v-for="(data, index) in this.dataRender"
                    :key="data.Id"
                    :class="{
                        'row-selected': isSelected(data.PropertyId),
                        'row-hovered': this.hoveredRowIndex === data.PropertyId,
                    }"
                    @click="clickOnRowTable(index, data.PropertyId, $event)"
                    class="table__content--row"
                    @mouseenter="handleMouseOver(data.PropertyId)"
                    @mouseleave="handleMouseOut"
                    @dblclick="showDetail(index, data.PropertyId)"
                >
                    <div class="text-align-center cell--item cell--item--checkbox" style="minWidth: 50px">
                        <m-checkbox
                            :ref="`checkbox-${data.PropertyId}`"
                            @checkedCheckbox="checkedCheckbox"
                            @uncheckedCheckbox="uncheckedCheckbox"
                            :id="data.PropertyId"
                        ></m-checkbox>
                    </div>
                    <div class="text-align-center cell--item" style="minWidth: 50px">{{ index + 1 }}</div>
                    <div class="text-align-left cell--item" style="minWidth: 150px">{{ data.PropertyCode }}</div>
                    <div class="text-align-left cell--item" style="minWidth: 300px">{{ data.PropertyName }}</div>
                    <div class="text-align-left cell--item" style="minWidth: 250px">
                        {{ data.PropertyTypeName }}
                    </div>
                    <div class="text-align-left cell--item" style="minWidth: 250px">{{ data.DepartmentName }}</div>
                    <div class="text-align-right cell--item" style="minWidth: 160px">
                        {{ this.formatedMoney(data.Quantity) }}
                    </div>
                    <div class="text-align-right cell--item" style="minWidth: 150px">
                        {{ this.formatedMoney(data.OriginalPrice) }}
                    </div>
                    <div class="text-align-right cell--item" style="minWidth: 150px">
                        {{ this.formatedMoney(data.WearRateValue) }}
                    </div>
                    <div class="text-align-right cell--item" style="minWidth: 150px">
                        {{ this.formatedMoney(data.OriginalPrice - data.WearRateValue) }}
                    </div>
                    <div class="table-list-icons cell--item stay-right" style="minWidth: 120px"></div>
                </div>
            </div>
            <div class="table__content--sumary">
                <div class="text-align-center cell--item" style="minWidth: 50px"></div>
                <div style="minWidth: 50px"></div>
                <div style="minWidth: 150px"></div>
                <div style="minWidth: 300px"></div>
                <div style="minWidth: 250px"></div>
                <div style="minWidth: 250px"></div>
                <div style="minWidth: 160px; font-weight: bold; padding: 0px 16px" class="text-align-right">
                    {{ this.formatedMoney(this.totalProperties['Quantity']) }}
                </div>
                <div style="minWidth: 150px; font-weight: bold; padding: 0px 16px" class="text-align-right">
                    {{ this.formatedMoney(this.totalProperties['OriginalPrice']) }}
                </div>
                <div style="minWidth: 150px; font-weight: bold; padding: 0px 16px" class="text-align-right">
                    {{ this.formatedMoney(this.totalProperties['WearRateValue']) }}
                </div>
                <div style="minWidth: 150px; font-weight: bold; padding: 0px 16px" class="text-align-right">
                    {{
                        this.formatedMoney(
                            this.totalProperties['OriginalPrice'] - this.totalProperties['WearRateValue'],
                        )
                    }}
                </div>
                <div style="minWidth: 120px"></div>
            </div>
            <!-- <el-skeleton :rows="5" animated style="width: 100% !important"/> -->
        </div>
        <div class="table__fixed">
            <div class="table__fixed--header">Chức năng</div>
            <div class="table__fixed--body" ref="fixedBody" @scroll="scrollHandler($event)">
                <div
                    v-for="(data, index) in this.dataRender"
                    :key="index"
                    :class="{
                        'row-selected': isSelected(data.PropertyId),
                        'row-hovered': this.hoveredRowIndex === data.PropertyId,
                    }"
                    class="table-list-icons cell--item"
                    @mouseover="handleMouseOver(data.PropertyId)"
                    @mouseout="handleMouseOut"
                    @click="clickOnRowTable(index, data.PropertyId, $event)"
                >
                    <!-- <el-tooltip effect="dark" content="Chỉnh sửa" placement="bottom-start"> -->
                    <div class="table--icon table--icon-pencil" @click="this.showDetail(index, data.id)"></div>
                    <!-- </el-tooltip> -->
                    <!-- <el-tooltip effect="dark" content="Xóa" placement="bottom-start"> -->
                    <div class="table--icon table--icon-comment"></div>
                    <!-- </el-tooltip> -->
                </div>
            </div>
            <div class="table__fixed--sumary"></div>
        </div>
        <div class="table__paging paging">
            <m-pagination
                :dataSelect="this.numberOfRecordsPerPage"
                :numberPages="this.pageNumber * 10"
                @changeCurrentPage="changeCurrentPage"
                :pageSize="this.pageSize"
                :totalRecords="this.totalRecords"
                @changePageSize="changePageSize"
            ></m-pagination>
        </div>
        <div class="table__content--empty" v-if="isShowEmptyRecord"></div>
        <!-- <div class="scroll-bar-y" ref="tableScroll" @scroll="scrollHandler">
            <div class="scroll-bar-content" :style="{ height: `${this.scrollBarContentHeight}px` }"></div>
        </div> -->
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

        <m-modal v-if="this.isShowDeleteDialog">
            <m-dialog
                :text="this.textDialog"
                firstBtnLabel="Không"
                thirdBtnLabel="Xóa"
                :beginText="this.beginText"
                :endText="this.deleteMsg"
                :firstBtnFunction="closeDialog"
                :thirdBtnFunction="deleteRow"
            ></m-dialog>
        </m-modal>
        <m-modal v-if="this.isShowWarnDialog">
            <m-dialog
                :text="this.textDialog"
                thirdBtnLabel="Đóng"
                :thirdBtnFunction="
                    () => {
                        this.isShowWarnDialog = false;
                    }
                "
            ></m-dialog>
        </m-modal>
    </div>
    <PropertyAdd
        v-if="isShowAddProperty"
        @addNewProperty="addNewProperty"
        @updateValueRow="updateValueRowSelected"
        @hideAddProperty="hideAddProperty"
        :selectedRow="this.selectedData"
        @resetSelectedRow="this.resetSelectedRow"
        :departmentCodes="this.departmentCodes"
        :propertyTypeCodes="this.propertyTypeCodes"
    ></PropertyAdd>
    <m-toast :label="this.labelToastSuccess" icon="icon--success" v-if="isShowToastSuccess"></m-toast>
    <m-toast :label="this.labelToastError" icon="icon--error" v-if="isShowToastError"></m-toast>
</template>

<script scoped>
import { formatMoney } from '@/common/common';
import PropertyAdd from './PropertyAdd.vue';
import instance from '@/common/instance';
import request from '@/common/api';
import debounce from 'lodash/debounce';
import { MISAResource } from '@/common/resource';
export default {
    name: 'EstateList',
    components: {
        PropertyAdd,
    },
    data() {
        return {
            MISAResource: MISAResource,
            scrollBarContentHeight: 0,
            listHeader: [],

            // giá trị ô lọc mã tài sản
            propertyTypeFilter: '',
            // giá trị ô lọc tên phòng ban sử dụng
            departmentUseFilter: '',
            //Danh sách bộ phận sử dụng
            Departments: [],
            // Danh sách loại tài sản
            PropertyTypes: [],
            // Giá trị ô tìm kiếm
            searchInput: '',
            // lưu các mã phòng ban để đẩy vào cho component combobox
            departmentCodes: [],
            // lưu các mã loại tài sản để đẩy vào cho component combobox
            propertyTypeCodes: [],

            isShowDetail: false,
            isShowToastSuccess: false,
            isShowToastError: false,

            labelToastSuccess: '',
            labelToastError: '',

            isShowAddProperty: false,
            isShowDeleteDialog: false,
            isShowWarnDialog: false,
            deleteMsg: '',
            beginText: '',
            textDialog: '',
            selectedData: null,
            hoveredRow: null,
            selectedRow: [],
            seletedRowIndex: null,
            idSelectedData: -1,
            pageSize: 20,
            totalRecords: 0,
            currentPage: 1,
            pageNumber: 0,
            isLoadingData: false,
            totalProperties: {}, // lưu giá trị của tổng các cột
            dataTable: [],
            dataRender: [],
            hoveredRowIndex: -1,

            // show khi không có bản ghi nào
            isShowEmptyRecord: false,

            numberOfRecordsPerPage: [
                {
                    label: 20,
                    value: 20,
                },
                {
                    label: 40,
                    value: 40,
                },
                {
                    label: 60,
                    value: 60,
                },
            ],
            // Đánh dấu hàng đầu tiên được chọn khi dùng sự kiện shift + click
            firstClickRow: 0,
            secondClickRow: '',
        };
    },
    async created() {
        // Lấy ra list header
        this.listHeader = this.MISAResource['vn-VI'].listHeader;

        this.getAllProperty();
        // Lấy ra danh sách phòng ban để đưa vào combo box
        this.getAllDepartments();

        // Lấy ra danh sách phòng ban để đưa vào combo box
        this.getAllPropertyTypes();

        await this.getPropertyWithFilter();

        this.calculateNumberPage();

        // Tính lại độ dài thanh scroll custom
        if (this.totalRecords) {
            if (this.totalRecords <= this.pageSize) {
                this.scrollBarContentHeight = this.totalRecords * 48;
            } else {
                this.scrollBarContentHeight = this.pageSize * 48;
            }
        }
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

            // tính toán lại giá trị thanh scroll
            if (this.totalRecords) {
                if (this.totalRecords <= newValue) {
                    this.scrollBarContentHeight = this.totalRecords * 48;
                } else {
                    this.totalRecords = newValue * 48;
                }
            }

            for (let i = 0; i < this.selectedRow.length; i++) {
                this.$refs[`checkbox-${this.selectedRow[i]}`][0].isChecked = true;
            }
            this.checkFullChecked();
        },
        currentPage: async function () {
            this.dataRender.forEach((data) => {
                this.dataTable.push(data);
            });
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
        dataRender: function () {
            this.calculateTotal(['Quantity', 'OriginalPrice', 'WearRateValue', 'ResidualValue']);
        },
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
                .catch((error) => {
                    console.log(error.message);
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
                .catch((error) => {
                    console.log(error.message);
                });

            this.PropertyTypes.forEach((propertyType) => {
                this.propertyTypeCodes.push({
                    id: propertyType.PropertyTypeId,
                    value: propertyType.PropertyTypeName,
                    label: propertyType.PropertyTypeCode,
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
                    `Property/filter?pageNumber=${this.currentPage}&pageSize=${this.pageSize}&searchInput=${this.searchInput}&propertyType=${this.propertyTypeFilter}&departmentName=${this.departmentUseFilter}`,
                )
                .then((response) => {
                    this.dataRender = response.data.Data;
                    this.totalRecords = response.data.NumberRecords;
                })
                .catch((error) => {
                    console.error(error);
                });

            setTimeout(() => {
                this.isLoadingData = false;
            }, 500);

            if (this.dataRender.length === 0) {
                this.isShowEmptyRecord = true;
            } else {
                this.isShowEmptyRecord = false;
            }
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
                .catch((error) => {
                    console.error(error);
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
        showDetail(index, id) {
            this.showAddProperty();
            this.selectedData = this.dataRender[index];
            this.seletedRowIndex = index;
            this.idSelectedData = id;
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
            }, 4000);
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
            }, 4000);
        },
        /*
         * Hiển thị trang thêm tài sản
         * Author: BATUAN (27/05/2023)
         */
        showAddProperty() {
            this.isShowAddProperty = true;
            this.$store.commit('toggleMaskElement');
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
            let isSuccess = false;
            // update giá trị trên db
            await request
                .updateRecord(`Property/${newValue.PropertyId}`, newValue)
                .then(() => {
                    isSuccess = true;
                })
                .catch((err) => {
                    this.showToastError(err.response.data.UserMessage);
                });

            if (isSuccess) {
                this.hideAddProperty();
                this.showToastSuccess(this.MISAResource['vn-VI'].updatePropertySuccess);
                this.getPropertyWithFilter();
            }
        },
        /*
         * Sự kiện click vào hàng của table
         * Author: BATUAN (12/06/2023)
         */
        clickOnRowTable(index, id, event) {
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
                    console.log(this.selectedRow);
                }
            } else if (event.shiftKey) {
                if (this.firstClickRow || this.firstClickRow === 0) {
                    this.secondClickRow = index;
                    if (this.secondClickRow > this.firstClickRow) {
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
            let isSuccess = false;
            await request
                .insertRecord('Property', newProperty)
                .then(() => (isSuccess = true))
                .catch((err) => {
                    this.showToastError(err.response.data.UserMessage);
                });

            if (isSuccess) {
                this.hideAddProperty();
                this.showToastSuccess(this.MISAResource['vn-VI'].addPropertySuccess);
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

            if (check == true) {
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
            }, 50);
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
         * Sự kiện khi click vào biểu tượng xóa
         * Author: BATUAN (08/06/2023)
         */
        deleteRowOnClickIcon() {
            // reset giá trị trong dialog
            this.textDialog = '';
            this.beginText = '';
            this.endText = '';

            if (this.selectedRow.length <= 0) {
                this.isShowWarnDialog = true;
                this.textDialog = this.MISAResource['vn-VI'].chooseAtLeastOne;
            } else {
                this.isShowDeleteDialog = true;
                if (this.selectedRow.length == 1) {
                    this.textDialog = this.MISAResource['vn-VI'].confirm;

                    let record = this.dataTable.find((data) => {
                        return data.PropertyId == this.selectedRow[0];
                    });
                    let code = record.PropertyCode;
                    let name = record.PropertyName;

                    this.deleteMsg = `${code}-${name}`;
                } else {
                    this.deleteMsg = '';
                    this.beginText =
                        this.selectedRow.length >= 10 ? this.selectedRow.length : `0${this.selectedRow.length}`;
                    this.textDialog = this.MISAResource['vn-VI'].deleteMultipleMessage;
                }
            }
        },
        /*
         * Đóng dialog thông báo xóa
         * Author: BATUAN (08/06/2023)
         */
        closeDialog() {
            this.isShowDeleteDialog = false;
        },
        /*
         * Xóa các hàng được chọn
         * Author: BATUAN (08/06/2023)
         */
        async deleteRow() {
            const idsToDelete = [];
            this.selectedRow.forEach((item) => {
                idsToDelete.push(item);
            });

            //Xóa các bản ghi
            await instance
                .delete(`Property/${idsToDelete.join(', ')}`)
                .then((res) => console.log(res))
                .catch((err) => console.log(err));

            // Ẩn dialog thông báo xóa
            this.isShowDeleteDialog = false;
            // Hiện toast message thông báo thành công
            this.showToastSuccess('Xóa thành công');
            // Reset list các hàng được chọn
            for (let i = 0; i < this.selectedRow.length; i++) {
                if (this.$refs[`checkbox-${this.selectedRow[i]}`] && this.$refs[`checkbox-${this.selectedRow[i]}`][0])
                    this.$refs[`checkbox-${this.selectedRow[i]}`][0].isChecked = false;
            }
            this.selectedRow = [];

            // this.getAllProperty();
            await this.getPropertyWithFilter();

            this.calculateNumberPage();

            this.$refs['checkbox-all'][0].isChecked = false;
        },
        /*
         * Sự kiện khi thay đổi giá trị của currentPage khi click sang trái hoặc phải ở phần paging
         * Author: BATUAN (07/06/2023)
         */
        changeCurrentPage(newPage) {
            this.currentPage = newPage;
        },
        /**
         * Tính toán tổng các giá trị của các hàng trong bảng (Số lượng, Nguyên giá
         * HM/KH lũy kế, Giá trị còn lại)
         * Author: BATUAN (08/06/2023)
         */
        calculateTotal(cols) {
            try {
                cols.forEach((col) => {
                    this.totalProperties[col] = this.dataRender.reduce((total, item) => {
                        return total + item[col];
                    }, 0);
                });
            } catch (e) {
                console.log(e);
            }
        },
        /*
         * Sự kiện khi scroll trong body của table
         * Author: BATUAN (14/06/2023)
         */
        scrollHandler: function (event) {
            const fixedBody = this.$refs.fixedBody;
            const contentBody = this.$refs.contentBody;
            if (event.target == fixedBody) {
                contentBody.scrollTop = fixedBody.scrollTop;
            } else {
                fixedBody.scrollTop = contentBody.scrollTop;
            }
        },
        /*
         * Sự kiện khi click vào 1 hàng trong table
         * Author: BATUAN (14/06/2023)
         */
        handleClickOnRow(index, id) {
            // cập nhật lại con trỏ trỏ tới vị trí đầu tiên khi dùng Shift + Click
            this.firstClickRow = index;

            if (this.selectedRow.length == 1 && this.selectedRow[0] == id) {
                this.selectedRow = this.selectedRow.filter(
                    (item) => !this.dataRender.some((obj) => obj.PropertyId === item),
                );
            } else {
                // for (let i = 0; i < this.selectedRow.length; i++) {
                //     this.$refs[`checkbox-${this.selectedRow[i]}`][0].isChecked = false;
                // }
                this.selectedRow = this.selectedRow.filter(
                    (item) => !this.dataRender.some((obj) => obj.PropertyId === item),
                );

                this.selectedRow.push(id);
            }
        },
        /*
         * Phương thức liên quan đến sự kiện check cho các ô check box khi các  hàng được chọn thay đổi
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
    },
};
</script>

<style>
@import url(../../css/elementui/el-select.css);
@import url(../../css/components/tooltip.css);
</style>
