<template>
    <div class="content__navbar">
        <div class="content__navbar-left">
            <div class="text-field">
                <m-text-input
                    v-model="this.searchInput"
                    placeholder="Tìm kiếm tài sản"
                    individualClass="text__input-icon-start"
                ></m-text-input>
            </div>
            <el-select
                v-model="propertyTypeFilter"
                clearable
                placeholder="Loại tài sản"
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
                placeholder="Bộ phận sử dụng"
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
        </div>
        <div class="content__navbar-right">
            <m-button label="+ Thêm tài sản" :individualClass="'btn--primary'" @click="this.showAddProperty"></m-button>
            <el-tooltip effect="dark" content="Xuất file excel" placement="bottom-start">
                <m-button :individualClass="' btn--single btn--combo-excel'"></m-button>
            </el-tooltip>
            <el-tooltip effect="dark" content="Xóa" placement="bottom-start">
                <m-button
                    :individualClass="' btn--single btn--combo-delete'"
                    @click="this.deleteRowOnClickIcon"
                    data-title="Xóa"
                ></m-button>
            </el-tooltip>
        </div>
    </div>
    <div class="content__body">
        <div class="table">
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
                            <div                                 :class="header.align"
                            >                                {{ header.name }}
                        </div>
                            </el-tooltip>
                            <div v-else :class="header.align">
                                {{ header.name }}
                            </div>
                        </template>
                    </div>
                </div>
                <div class="table__content--body" ref="contentBody">
                    <div
                        v-for="(data, index) in this.dataRender"
                        :key="data.Id"
                        :class="{ 'row-selected': isSelected(index), 'row-hovered': this.hoveredRowIndex===index }"
                        @click="clickOnRowTable(index, $event)"
                        class="table__content--row"
                        @mouseover="handleMouseOver(index)"
                        @mouseout="handleMouseOut"
                    >
                        <div class="text-align-center cell--item cell--item--checkbox" style="minWidth: 50px">
                            <m-checkbox
                                :ref="`checkbox-${index}`"
                                @checkedCheckbox="checkedCheckbox"
                                @uncheckedCheckbox="uncheckedCheckbox"
                                :index="index"
                            ></m-checkbox>
                        </div>
                        <div class="text-align-center cell--item" style="minWidth: 50px">{{ index + 1 }}</div>
                        <div class="text-align-left cell--item" style="minWidth: 150px">{{ data.PropertyCode }}</div>
                        <div class="text-align-left cell--item" style="minWidth: 300px">{{ data.PropertyName }}</div>
                        <div class="text-align-left cell--item" style="minWidth: 300px">
                            {{ data.PropertyTypeName }}
                        </div>
                        <div class="text-align-left cell--item" style="minWidth: 300px">{{ data.DepartmentName }}</div>
                        <div class="text-align-right cell--item" style="minWidth: 160px">{{ data.Quantity }}</div>
                        <div class="text-align-right cell--item" style="minWidth: 200px">
                            {{ this.formatedMoney(data.OriginalPrice) }}
                        </div>
                        <div class="text-align-right cell--item" style="minWidth: 300px">
                            {{ this.formatedMoney(data.WearRateValue) }}
                        </div>
                        <div class="text-align-right cell--item" style="minWidth: 150px">
                            {{ this.formatedMoney(data.OriginalPrice - data.WearRateValue) }}
                        </div>
                        <div class="table-list-icons cell--item" style="minWidth: 120px">
                            <el-tooltip effect="dark" content="Chỉnh sửa" placement="bottom-start">
                                <div
                                    class="table--icon table--icon-pencil"
                                    @click="this.showDetail(index, data.id)"
                                ></div>
                            </el-tooltip>
                            <el-tooltip effect="dark" content="Xóa" placement="bottom-start">
                                <div
                                    class="table--icon table--icon-delete"
                                    @click="this.duplicateRow(index)"
                                ></div>
                            </el-tooltip>
                        </div>
                    </div>
                </div>
                <div class="table__content--sumary">
                    <div class="text-align-center cell--item" style="minWidth: 50px"></div>
                    <div style="minWidth: 50px"></div>
                    <div style="minWidth: 150px"></div>
                    <div style="minWidth: 300px"></div>
                    <div style="minWidth: 300px"></div>
                    <div style="minWidth: 300px"></div>
                    <div style="minWidth: 160px; font-weight: bold; padding: 0px 16px;" class="text-align-right">
                        {{ this.formatedMoney(this.totalProperties['Quantity']) }}
                    </div>
                    <div style="minWidth: 200px; font-weight: bold; padding: 0px 16px;" class="text-align-right">
                        {{ this.formatedMoney(this.totalProperties['OriginalPrice']) }}
                    </div>
                    <div style="minWidth: 300px; font-weight: bold; padding: 0px 16px;" class="text-align-right">
                        {{ this.formatedMoney(this.totalProperties['WearRateValue']) }}
                    </div>
                    <div style="minWidth: 150px; font-weight: bold; padding: 0px 16px;" class="text-align-right">
                        {{
                            this.formatedMoney(
                                this.totalProperties['OriginalPrice'] - this.totalProperties['WearRateValue'],
                            )
                        }}
                    </div>
                    <div style="minWidth: 120px"></div>
                </div>
                <div class="table__fixed">
                    <div class="table__fixed--header">Chức năng</div>
                    <div class="table__fixed--body" ref="fixedBody">
                        <div                         :class="{ 'row-selected': isSelected(index), 'row-hovered': this.hoveredRowIndex===index }"
                        v-for="(data, index) in this.dataRender" :key="index" class="table-list-icons cell--item" 
                        @mouseover="handleMouseOver(index)"
                        @mouseout="handleMouseOut"
                        >
                            <el-tooltip effect="dark" content="Chỉnh sửa" placement="bottom-start">
                                <div
                                    class="table--icon table--icon-pencil"
                                    @click="this.showDetail(index, data.id)"
                                ></div>
                            </el-tooltip>
                            <el-tooltip effect="dark" content="Xóa" placement="bottom-start">
                                <div
                                    class="table--icon table--icon-delete"
                                    @click="this.duplicateRow(index)"
                                ></div>
                            </el-tooltip>
                        </div>
                    </div>
                    <div class="table__fixed--sumary"></div>
                </div>
                <div class="scroll-bar-y" ref="tableScroll" @scroll="scrollHandler">
                    <div class="scroll-bar-content" :style="{ height: `${this.scrollBarContentHeight}px` }"></div>
                </div>
            </div>

            <!-- <div class="table--content-fixed">
                <div class="fixed-header"></div>
                <div class="fixed-body"></div>
            </div> -->
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
        <div class="paging" style="display: none">
            <!-- <div class="table__paging-total">
                Tổng số:&nbsp;&nbsp;<b>{{ this.totalRecords }}</b> bản ghi
            </div>
            <div class="table__paging-numberrecords">
                <m-combo-box
                    class="combo-box--small"
                    style="height: 32px !important; border: none !important"
                    :dataSelect="this.numberOfRecordsPerPage"
                    v-model="pageSize"
                ></m-combo-box>
            </div> -->

            
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
        <!-- <div v-show="this.isLoadingData" class="table--loading"><font-awesome-icon icon="fa-solid fa-spinner" /></div> -->
    </div>
    <PropertyAdd
        v-if="isShowAddProperty"
        @hideAddProperty="hideAddProperty"
        @addNewProperty="addNewProperty"
        @showToastSuccess="showToastSuccess"
        @updateValueRow="updateValueRowSelected"
        :selectedRow="this.selectedData"
        @resetSelectedRow="this.resetSelectedRow"
        :departmentCodes="this.departmentCodes"
        :propertyTypeCodes="this.propertyTypeCodes"
    ></PropertyAdd>
    <m-toast
        :label="this.labelToastSuccess"
        icon="icon--success"
        v-if="isShowToastSuccess"
        @showToastSuccess="showToastSuccess"
    ></m-toast>
</template>

<script scoped>
import { formatMoney } from '@/common/common';
import PropertyAdd from './PropertyAdd.vue';
import instance from '@/common/instance';

export default {
    name: 'EstateList',
    components: {
        PropertyAdd,
    },
    data() {
        return {
            scrollBarContentHeight: 0,
            listHeader: [
                {
                    name: 'checkbox',
                    width: '50px',
                    align: "text-align-center",
                },
                {
                    name: 'STT',
                    width: '50px',
                    fullName: 'Số thứ tự',
                    align: "text-align-center",
                },
                {
                    name: 'Mã tài sản',
                    width: '150px',
                    align: "text-align-left",
                },
                {
                    name: 'Tên tài sản',
                    width: '300px',
                    align: "text-align-left",
                },
                {
                    name: 'Loại tài sản',
                    width: '300px',
                    align: "text-align-left",
                },
                {
                    name: 'Bộ phận sử dụng',
                    width: '300px',
                    align: "text-align-left",
                },
                {
                    name: 'Số lượng',
                    width: '160px',
                    align: "text-align-right",
                },
                {
                    name: 'Nguyên giá',
                    width: '200px',
                    align: "text-align-right",
                },
                {
                    name: 'HM/ KH lũy kế',
                    width: '300px',
                    fullName: 'Hao mòn/ Khấu hao lũy kế',
                    align: "text-align-right",
                },
                {
                    name: 'Giá trị còn lại',
                    width: '150px',
                    align: "text-align-right",
                },
                {
                    name: 'Chức năng',
                    width: '120px',
                    align: "text-align-right",
                },
            ],

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

            labelToastSuccess: '',

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
            isLoadingData: true,
            totalProperties: {}, // lưu giá trị của tổng các cột
            dataTable: [],
            dataRender: [],
            hoveredRowIndex: -1,
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
            firstClickRow: '',
            secondClickRow: '',
        };
    },
    async created() {
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
            }
            else {
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
            for (let i = 0 ; i < this.selectedRow.length; i++) {
                this.$refs[`checkbox-${this.selectedRow[i]}`][0].isChecked = false;
            }
            this.$refs['checkbox-all'][0].isChecked = false;
            this.selectedRow = [];

            // tính toán lại giá trị thanh scroll
            if (this.totalRecords) {
            if (this.totalRecords <= newValue) {
                this.scrollBarContentHeight = this.totalRecords * 48;
            }
            else {
                this.totalRecords = newValue * 48;
            }
        }
        },
        currentPage: function () {
            this.getPropertyWithFilter();
        },
        propertyTypeFilter: async function (newValue) {
            const params = new URLSearchParams();
            if (newValue) {
                params.append('PropertyType', newValue);
            }
            if (this.departmentUseFilter) {
                params.append('DepartmentUse', this.departmentUseFilter);
            }
            await instance
                .get(`property?${params}`)
                .then((response) => {
                    this.dataTable = response.data;
                })
                .catch((error) => {
                    console.log(error);
                });
            this.calculateNumberPage();

            this.getPropertyWithFilter();
        },
        departmentUseFilter: async function (newValue) {
            const params = new URLSearchParams();
            if (newValue) {
                params.append('DepartmentUse', newValue);
            }
            if (this.propertyTypeFilter) {
                params.append('PropertyType', this.propertyTypeFilter);
            }
            await instance
                .get(`property?${params}`)
                .then((response) => {
                    this.dataTable = response.data;
                })
                .catch((error) => {
                    console.log(error);
                });
            this.calculateNumberPage();

            this.getPropertyWithFilter();
        },
        searchInput: function () {
            this.getPropertyWithFilter();
        },
        dataTable: function () {
            this.calculateNumberPage();
        },
        dataRender: function () {
            this.calculateTotal(['Quantity', 'OriginalPrice', 'WearRateValue', 'ResidualValue']);
        }
    },
    methods: {
        handleMouseWheel() {
    //   this.$refs.contentBody.scrollTop += event.deltaY;
    //   this.$refs.fixedBody.scrollTop += this.$refs.contentBody.scrollTop;
    //   this.$refs.tableScroll.scrollTop += event.deltaY;
    },
        changePageSize: function(newValue) {
            this.pageSize = newValue;
        },
        /*
         * Lấy dữ liệu phòng ban để đưa vào combobox
         * Author: BATUAN (14/06/2023)
         */
        async getAllDepartments() {
            await instance
                .get('Department')
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
            await instance
                .get('PropertyType')
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
            await instance
                .get(
                    `Property/filter?pageNumber=${this.currentPage}&pageSize=${this.pageSize}&searchInput=${this.searchInput}&propertyType=${this.propertyTypeFilter}&departmentName=${this.departmentUseFilter}`,
                )
                .then((response) => {
                    this.dataRender = response.data.Data;
                    this.totalRecords = response.data.NumberRecords;
                })
                .catch((error) => {
                    console.error(error);
                });

            this.isLoadingData = false;
        },
        /*
         * Lấy dữ liệu tất cả bản ghi trong db
         * Author: BATUAN (08/06/2023)
         */
        async getAllProperty() {
            await instance
                .get('Property')
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
            this.isShowAddProperty = true;
            this.selectedData = this.dataRender[index];
            this.seletedRowIndex = index;
            this.idSelectedData = id;
        },
        /*
         * Nhân đôi hàng được chọn
         * Author: BATUAN (01/06/2023)
         */
        duplicateRow(index) {
            const row = this.dataTable[index];
            const duplicateRow = { ...row };
            this.dataTable.splice(index + 1, 0, duplicateRow);
        },
        /*
         * Ẩn trang chi tiết
         * Author: BATUAN (27/05/2023)
         */
        closeDetail() {
            this.isShowDetail = false;
        },
        /*
         * Show toast hiển thị lưu dữ liệu thành công
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
         * Hiển thị trang thêm tài sản
         * Author: BATUAN (27/05/2023)
         */
        showAddProperty() {
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
            // update giá trị trên db
            await instance
                .put(`Property/${newValue.PropertyId}`, newValue)
                .then((res) => console.log(res))
                .catch((err) => {
                    console.log(err);
                });
            // cập nhật lại bảng
            this.getPropertyWithFilter();
        },
        /*
         * Sự kiện click vào hàng của table
         * Author: BATUAN (12/06/2023)
         */
        clickOnRowTable(index, event) {
            if (event.ctrlKey) {
                
                if (!this.selectedRow.includes(index)) {
                    this.selectedRow.push(index);
                    this.$refs[`checkbox-${index}`][0].isChecked = true;
                    if (this.selectedRow.length == this.dataRender.length) {
                        this.$refs['checkbox-all'].isChecked = true;
                    }
                } else {
                    this.selectedRow = this.selectedRow.filter((num) => {
                        if (num == index) {
                            this.$refs[`checkbox-${num}`][0].isChecked = false;
                            this.$refs['checkbox-all'].isChecked = false;
                        }
                        return num != index;
                    });
                }
            } else if (event.shiftKey) {
                if (this.firstClickRow || this.firstClickRow === 0) {
                    this.secondClickRow = index;
                    let saveRow = [];
                    if (this.secondClickRow > this.firstClickRow) {
                        for (let i = this.firstClickRow; i <= this.secondClickRow; i++) {
                            saveRow.push(i);
                            if (!this.selectedRow.includes(i)) {
                                this.selectedRow.push(i);
                            }
                            this.$refs[`checkbox-${i}`][0].isChecked = true;
                        }
                    } else {
                        for (let i = this.secondClickRow; i <= this.firstClickRow; i++) {
                            saveRow.push(i);
                            if (!this.selectedRow.includes(i)) {
                                this.selectedRow.push(i);
                            }
                            this.$refs[`checkbox-${i}`][0].isChecked = true;
                        }
                    }
                    this.selectedRow = this.selectedRow.filter((element) => saveRow.includes(element));
                    for (let i = 0; i < this.dataRender.length; i++) {
                        if (!this.selectedRow.includes(i)) {
                            this.$refs[`checkbox-${i}`][0].isChecked = false;
                        }
                    }
                    if (this.selectedRow.length == this.dataRender.length) {
                        this.$refs['checkbox-all'].isChecked = true;
                    } else {
                        this.$refs['checkbox-all'].isChecked = false;
                    }
                } else {
                    this.firstClickRow = index;
                    if (!this.selectedRow.includes(index)) {
                        this.selectedRow.push(index);
                        this.$refs[`checkbox-${index}`][0].isChecked = true;
                    }
                }
            }
            else {
                this.handleClickOnRow(index);
            }
        },
        /*
         * Thêm một bản ghi mới vào dữ liệu
         * Author: BATUAN (14/06/2023)
         */
        async addNewProperty(newProperty) {
            await instance
                .post('Property', newProperty)
                .then((res) => console.log(res))
                .catch((err) => {
                    console.log(err);
                });

            // this.getAllProperty();
            await this.getPropertyWithFilter();

            this.calculateNumberPage();
        },
        /*
         * Format giá trị tiền
         * Author: BATUAN (27/05/2023)
         */
        formatedMoney(value) {
            return formatMoney(value);
        },
        resetSelectedRow() {
            this.selectedData = null;
        },
        /*
         * Sự kiện khi ô checkbox được check
         * Author: BATUAN (08/06/2023)
         */
        checkedCheckbox(index) {
            this.selectedRow.push(index);
            if (this.selectedRow.length == this.dataRender.length) {
                this.$refs['checkbox-all'].isChecked = true;
            }
        },
        /*
         * Sự kiện khi ô checkbox uncheck
         * Author: BATUAN (08/06/2023)
         */
        uncheckedCheckbox(index) {
            this.selectedRow = this.selectedRow.filter((item) => item !== index);
            this.$refs['checkbox-all'].isChecked = false;
        },
        isSelected(index) {
            if (this.selectedRow) {
                return this.selectedRow.includes(index);
            }
            return false;
        },
        handleMouseOver(index) {
            this.hoveredRowIndex = index;
        },
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
                    if (!this.selectedRow.includes(i)) {
                        this.selectedRow.push(i);
                        this.selectedRow.sort(function (a, b) {
                            return a - b;
                        });
                        targetRef.push(`checkbox-${i}`);
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
            this.selectedRow = [];
            let targetRef = [];
            if (this.dataRender) {
                for (let i = 0; i < this.dataRender.length; i++) {
                    targetRef.push(`checkbox-${i}`);
                }
            }
            targetRef.forEach((targetRef) => {
                this.$refs[targetRef][0].isChecked = false;
            });
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
                this.textDialog = 'Vui lòng chọn ít nhất 1 bản ghi!';
            } else {
                this.isShowDeleteDialog = true;
                if (this.selectedRow.length == 1) {
                    this.textDialog = 'Bạn có muốn xóa tài sản';
                    this.deleteMsg = `<<${this.dataRender[this.selectedRow[0]].PropertyCode}-${
                        this.dataRender[this.selectedRow[0]].PropertyName
                    }>>`;
                } else {
                    this.deleteMsg = '';
                    this.beginText =
                        this.selectedRow.length > 10 ? this.selectedRow.length : `0${this.selectedRow.length}`;
                    this.textDialog = ' tài sản đã được chọn. Bạn có muốn xóa các tài sản này khỏi danh sách?';
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
                idsToDelete.push(this.dataRender[item].PropertyId);
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
            for (let i = 0 ; i < this.selectedRow.length; i++) {
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

            for (let i = 0 ; i < this.selectedRow.length; i++) {
                this.$refs[`checkbox-${this.selectedRow[i]}`][0].isChecked = false;
            }
            this.selectedRow = [];
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

        scrollHandler:(function() {
            this.$refs.contentBody.scrollTop = this.$refs.tableScroll.scrollTop;
            this.$refs.fixedBody.scrollTop = this.$refs.tableScroll.scrollTop;
        }),

        handleClickOnRow(index) {
            // cập nhật lại con trỏ trỏ tới vị trí đầu tiên khi dùng Shift + Click
            this.firstClickRow = index;

            for (let i = 0 ; i < this.selectedRow.length; i++) {
                this.$refs[`checkbox-${this.selectedRow[i]}`][0].isChecked = false;
            }
            this.selectedRow = [];

            this.selectedRow.push(index);
            this.$refs[`checkbox-${index}`][0].isChecked = true;
            this.$refs["checkbox-all"][0].isChecked = false;
        }
    },
};
</script>

<style>
@import url('../../css/elementui/el-select.css');
@import url(../../css/components/tooltip.css);
</style>
