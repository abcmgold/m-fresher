<template>
    <div class="content__navbar">
        <div class="content__navbar-left">
            <div class="text-field">
                <m-text-input
                    v-model="this.filterText"
                    placeholder="Tìm kiếm tài sản"
                    individualClass="text__input-icon-start"
                    @input="this.onChangeInputSearch"
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
                    v-for="item in PropertyTypeCodes"
                    :key="item.value"
                    :label="item.label"
                    :value="item.value"
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
                    v-for="item in DepartmentCodes"
                    :key="item.value"
                    :label="item.label"
                    :value="item.value"
                ></el-option>
            </el-select>
        </div>
        <div class="content__navbar-right">
            <m-button label="+ Thêm tài sản" :individualClass="'btn--primary'" @click="this.showAddProperty"></m-button>
            <m-button :individualClass="' btn--single btn--combo-excel'" title="Xuất file excel"></m-button>
            <m-button
                :individualClass="' btn--single btn--combo-delete'"
                @click="this.deleteRowOnClickIcon"
                title="Xóa"
            ></m-button>
        </div>
    </div>
    <div class="content__body">
        <table class="table">
            <thead>
                <tr>
                    <th class="left-permanent text-align-center" type="checkbox" style="width: 50px">
                        <m-checkbox
                            ref="checkbox-all"
                            @selectAllRow="selectAllRow"
                            @unSelectAllRow="unSelectAllRow"
                            type="primary"
                        ></m-checkbox>
                    </th>
                    <th class="text-align-left" model-name="Order" type="order" style="width: 50px">STT</th>
                    <th class="text-align-left" model-name="PropertyCode" style="width: 150px">Mã tài sản</th>
                    <th class="text-align-left" model-name="PropertyName" style="width: 300px">Tên tài sản</th>
                    <th class="text-align-left" model-name="PropertyType" style="width: 300px">Loại tài sản</th>
                    <th class="text-align-left" model-name="DepartmentUse" style="width: 300px">Bộ phận sử dụng</th>
                    <th class="text-align-right" model-name="Quantity" type="number" style="width: 160px">Số lượng</th>
                    <th class="text-align-right" model-name="OriginalValue" type="money" style="width: 200px">
                        Nguyên giá
                    </th>
                    <th
                        class="text-align-right"
                        model-name="Limit"
                        type="money"
                        style="width: 200px"
                        title="Hao mòn/ Khấu hao lũy kế"
                    >
                        HM/KH lũy kế
                    </th>
                    <th class="text-align-right" model-name="ResidualValue" type="money" style="width: 150px">
                        Giá trị còn lại
                    </th>
                    <th class="text-align-center" style="width: 150px">Chức năng</th>
                </tr>
            </thead>
            <tbody v-if="!this.isLoadingData">
                <tr
                    v-for="(data, index) in this.dataRender"
                    :key="data.Id"
                    @mouseover="showIcons(index)"
                    @mouseout="hideIcons()"
                    :class="{ 'row-selected': isSelected(index) }"
                >
                    <td class="text-align-center">
                        <m-checkbox
                            :ref="`checkbox-${index}`"
                            @checkedCheckbox="checkedCheckbox"
                            @uncheckedCheckbox="uncheckedCheckbox"
                            :index="index"
                        ></m-checkbox>
                    </td>
                    <td class="text-align-center">{{ index + 1 }}</td>
                    <td class="text-align-left">{{ data.PropertyCode }}</td>
                    <td class="text-align-left">{{ data.PropertyName }}</td>
                    <td class="text-align-left">{{ data.PropertyType }}</td>
                    <td class="text-align-left">{{ data.DepartmentUse }}</td>
                    <td class="text-align-right">{{ data.Quantity }}</td>
                    <td class="text-align-right">{{ this.formatedMoney(data.OriginalValue) }}</td>
                    <td class="text-align-right">{{ this.formatedMoney(data.Limit) }}</td>
                    <td class="text-align-right">{{ this.formatedMoney(data.ResidualValue) }}</td>
                    <td class="table-list-icons">
                        <div
                            class="table-icon table-icon-pencil"
                            v-show="isHovered(index)"
                            @click="this.showDetail(index, data.id)"
                        ></div>
                        <div
                            class="table-icon table-icon-comment"
                            v-show="isHovered(index)"
                            @click="this.duplicateRow(index)"
                        ></div>
                    </td>
                </tr>
                <div></div>
            </tbody>
            <tfoot>
                <tr>
                    <td colspan="6" class="table__paging">
                        <div class="table__paging-total">
                            Tổng số:&nbsp;&nbsp;<b>{{ this.totalRecords }}</b> bản ghi
                        </div>
                        <div class="table__paging-numberrecords">
                            <m-combo-box
                                class="combo-box--small"
                                style="height: 32px !important; border: none !important"
                                :dataSelect="this.numberOfRecordsPerPage"
                                v-model="numberRecords"
                            ></m-combo-box>
                        </div>

                        <m-pagination
                            :numberPages="this.numberPages * 10"
                            @changeCurrentPage="changeCurrentPage"
                        ></m-pagination>
                    </td>
                    <td class="text-align-right" model-name="Quantity">
                        {{ this.formatedMoney(this.totalProperties['Quantity']) }}
                    </td>
                    <td class="text-align-right" model-name="OriginalValue" type="money">
                        {{ this.formatedMoney(this.totalProperties['OriginalValue']) }}
                    </td>
                    <td class="text-align-right" model-name="Limit" type="money">
                        {{ this.formatedMoney(this.totalProperties['Limit']) }}
                    </td>
                    <td class="text-align-right" model-name="ResidualValue" type="money">
                        {{ this.formatedMoney(this.totalProperties['ResidualValue']) }}
                    </td>
                    <td></td>
                </tr>
            </tfoot>
        </table>
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
        <div v-show="this.isLoadingData" class="table--loading"><font-awesome-icon icon="fa-solid fa-spinner" /></div>
    </div>
    <PropertyAdd
        v-if="isShowAddProperty"
        @hideAddProperty="hideAddProperty"
        @addNewProperty="addNewProperty"
        @showToastSuccess="showToastSuccess"
        @updateValueRow="updateValueRowSelected"
        :selectedRow="this.selectedData"
        @resetSelectedRow="this.resetSelectedRow"
    ></PropertyAdd>
    <m-toast
        label="Lưu dữ liệu thành công"
        icon="icon--success"
        v-if="isShowToastSuccess"
        @showToastSuccess="showToastSuccess"
    ></m-toast>
</template>

<script scoped>
import { formatMoney } from '@/common/common';
import PropertyAdd from './PropertyAdd.vue';
import instance from '@/common/instance';
import { reactive } from 'vue';

export default {
    name: 'EstateList',
    components: {
        PropertyAdd,
    },
    data() {
        return {
            propertyTypeFilter: '',
            departmentUseFilter: '',
            DepartmentCodes: [
                { value: 'Phòng Hành chính Tổng hợp', label: 'Phòng Hành chính Tổng hợp' },
                { value: 'Phòng Kế toán', label: 'Phòng Kế toán' },
                { value: 'Phòng Nhân sự', label: 'Phòng Nhân sự' },
            ],
            PropertyTypeCodes: [
                { value: 'Máy vi tính xách tay', label: 'Máy vi tính xách tay' },
                { value: 'Máy tính bàn', label: 'Máy tính bàn' },
            ],
            isShowDetail: false,
            isShowToastSuccess: false,
            isShowAddProperty: false,
            isShowDeleteDialog: false,
            deleteMsg: '',
            beginText: '',
            textDialog: '',
            selectedData: null,
            hoveredRow: null,
            selectedRow: [],
            seletedRowIndex: null,
            idSelectedData: -1,
            numberRecords: 20,
            totalRecords: 0,
            currentPage: 1,
            numberPages: 0,
            isLoadingData: true,
            totalProperties: {}, // lưu giá trị của tổng các cột
            dataTable: [
                {
                    Id: 1,
                    PropertyCode: '55H7WN72/2022',
                    PropertyName: 'Dell Inspiron 3467',
                    PropertyType: 'Máy vi tính xách tay 1',
                    DepartmentCode: 'HCTH',
                    DepartmentUse: 'Phòng Hành chính Tổng hợp',
                    PropertyTypeCode: 'MTXT',
                    Quantity: 1,
                    OriginalValue: 20000000,
                    Limit: 894000,
                    ResidualValue: 19106000,
                    NumberYearsUse: 10,
                    FollowYear: 2021,
                    PurchaseDate: '2023-05-22',
                    UseDate: '2023-05-22',
                    WearRate: 6.67,
                },
                {
                    Id: 1,
                    PropertyCode: '55H7WN72/2022',
                    PropertyName: 'Dell Inspiron 3467',
                    PropertyType: 'Máy vi tính xách tay 1',
                    DepartmentCode: 'HCTH',
                    DepartmentUse: 'Phòng Hành chính Tổng hợp',
                    PropertyTypeCode: 'MTXT',
                    Quantity: 1,
                    OriginalValue: 20000000,
                    Limit: 894000,
                    ResidualValue: 19106000,
                    NumberYearsUse: 10,
                    FollowYear: 2021,
                    PurchaseDate: '2023-05-22',
                    UseDate: '2023-05-22',
                    WearRate: 6.67,
                },
            ],
            dataRender: [],
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
            filterText: '',
        };
    },
    created() {
        this.getAllProperty();
        this.calculateNumberPage();
        this.getPropertyWithFilter();

        console.log(this.dataTable)
    },
    watch: {
        numberRecords: function (newValue) {
            this.numberRecords = newValue;
            this.currentPage = 1;
            this.calculateNumberPage();
            this.getPropertyWithFilter();
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
        dataTable: function () {
            this.calculateNumberPage();
        },
        dataRender: function () {
            this.calculateTotal(['Quantity', 'OriginalValue', 'Limit', 'ResidualValue']);
        },
    },
    methods: {
        /*
         * Lấy dữ liệu của 1 trang có lọc dữ liệu theo combobox
         * Author: BATUAN (08/06/2023)
         */
        async getPropertyWithFilter() {
            this.isLoadingData = true;
            const params = new URLSearchParams();
            if (this.propertyTypeFilter) {
                params.append('PropertyType', this.propertyTypeFilter);
            }
            if (this.departmentUseFilter) {
                params.append('DepartmentUse', this.departmentUseFilter);
            }
            params.append('_page', this.currentPage);
            params.append('_limit', this.numberRecords);
            await instance
                .get(`property?${params}`)
                .then((response) => {
                    this.dataRender = response.data;
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
                .get(`property`)
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
            this.totalRecords = this.dataTable.length;
            this.numberPages = Math.ceil(this.totalRecords / this.numberRecords);
        },
        /*
         * Khi ô input tìm kiếm thay đổi
         * Author: BATUAN (08/06/2023)
         */
        onChangeInputSearch() {
            this.dataRender = reactive(this.dataRender);
            // lọc data render theo giá trị được tìm kiếm
            this.dataRender = this.dataTable.filter((item) => {
                return (
                    item.PropertyCode.toLowerCase().includes(this.filterText.toLowerCase().trim()) ||
                    item.PropertyName.toLowerCase().includes(this.filterText.toLowerCase().trim())
                );
            });
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
        showToastSuccess() {
            this.isShowToastSuccess = true;
            // Ẩn sau 1.5s
            setTimeout(() => {
                this.isShowToastSuccess = false;
            }, 1500);
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
         * Hiển thị icon khi hover vào hàng trong table
         * Author: BATUAN (27/05/2023)
         */
        showIcons(index) {
            this.hoveredRow = index;
        },
        hideIcons() {
            this.hoveredRow = null;
        },
        isHovered(index) {
            return this.hoveredRow === index;
        },
        /*
         * Update dữ liệu khi lưu dữ liệu trong form chỉnh sửa
         * Author: BATUAN (27/05/2023)
         */
        updateValueRowSelected(newValue) {
            // update giá trị trên db
            instance
                .put(`property/${this.idSelectedData}`, newValue)
                .then((res) => console.log(res))
                .catch((err) => {
                    console.log(err);
                });
            // cập nhật lại bảng
            this.getPropertyWithFilter();
        },
        /*
         * Thêm một bản ghi mới vào dữ liệu
         * Author: BATUAN (30/05/2023)
         */
        async addNewProperty(newProperty) {
            await instance
                .post('property', newProperty)
                .then((res) => console.log(res))
                .catch((err) => {
                    console.log(err);
                });

            this.getAllProperty();
            this.calculateNumberPage();
            this.getPropertyWithFilter();
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

            if (this.selectedRow.length <= 0) return;
            this.isShowDeleteDialog = true;
            if (this.selectedRow.length == 1) {
                this.textDialog = 'Bạn có muốn xóa tài sản';
                this.deleteMsg = `<<${this.dataRender[this.selectedRow[0]].PropertyCode}-${
                    this.dataRender[this.selectedRow[0]].PropertyName
                }>>`;
            } else {
                this.deleteMsg = '';
                this.beginText = this.selectedRow.length > 10 ? this.selectedRow.length : `0${this.selectedRow.length}`;
                this.textDialog = ' tài sản đã được chọn. Bạn có muốn xóa các tài sản này khỏi danh sách?';
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
                idsToDelete.push(this.dataRender[item].id);
                this.selectedRow = this.selectedRow.filter((i) => {
                    i != item;
                });
            });

            await instance
                .delete(`property/${idsToDelete.join(';')}`)
                .then((res) => console.log(res))
                .catch((err) => console.log(err));

            this.isShowDeleteDialog = false;

            this.getAllProperty();
            this.calculateNumberPage();
            this.getPropertyWithFilter();

            this.$refs['checkbox-all'].isChecked = false;
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
    },
};
</script>

<style>
@import url('../../css/elementui/el-select.css');
</style>
