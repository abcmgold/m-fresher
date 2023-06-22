<template>
    <table id="tbOverview" class="table">
        <thead>
            <tr>
                <th class="left-permanent text-align-center" type="checkbox" style="width: 50px">
                    <input type="checkbox" />
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
        <tbody>
            <tr
                v-for="(data, index) in this.datatable"
                :key="index"
                @dblclick="this.changeShowDetail(index)"
                @mouseover="showIcons(index)"
                @mouseout="hideIcons()"
            >
                <td class="text-align-center">
                    <input type="checkbox" />
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
                    <div class="table-icon table-icon-pencil" v-show="isHovered(index)"></div>
                    <div class="table-icon table-icon-comment" v-show="isHovered(index)"></div>
                </td>
            </tr>
        </tbody>
        <tfoot>
            <tr>
                <td colspan="6" class="table__paging">
                    <div class="table__paging-total">Tổng số:&nbsp;&nbsp;<b>200</b> bản ghi</div>
                    <div class="table__paging-numberrecords">
                        20
                        <div class="table__paging-numberrecords-icon"></div>
                    </div>
                    <div class="table__paging-bar">
                        <div class="table__paging-bar-icon-left"></div>
                        <ul class="table__paging-numbers">
                            <li class="paging-number selected">1</li>
                            <li class="paging-number">2</li>
                            <li class="paging-number">...</li>
                            <li class="paging-number">10</li>
                        </ul>
                        <div class="table__paging-bar-icon-right"></div>
                    </div>
                </td>
                <td class="text-align-right" model-name="Quantity">35</td>
                <td class="text-align-right" model-name="OriginalValue" type="money">905.000.000</td>
                <td class="text-align-right" model-name="Limit" type="money">41.088.000</td>
                <td class="text-align-right" model-name="ResidualValue" type="money">863.912.000</td>
                <td></td>
            </tr>
        </tfoot>
    </table>
</template>

<script>
import { formatMoney } from '../common/common.js';
import { reactive } from 'vue';

export default {
    name: 'MISATable',
    props: {
        dataTable: null,
    },
    data() {
        return {
            datatable: null,
            selectedData: null,
            hoveredRow: null,
            seletedRowIndex: null,
        };
    },
    created() {
        this.datatable = this.dataTable;
    },
    methods: {
        /*
         * Hiển thị trang chi tiết và gán các giá trị của hàng được chọn
         * Author: BATUAN (27/05/2023)
         */
        changeShowDetail(index) {
            this.isShowDetail = true;
            this.selectedData = this.dataTable[index];
            this.seletedRowIndex = index;
        },
        /*
         * Ẩn trang chi tiết
         * Author: BATUAN (27/05/2023)
         */
        changeHideDetail() {
            this.isShowDetail = false;
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
            // Khởi tạo dữ liệu reactive
            this.datatable = reactive(this.dataTable);

            // Thay đổi giá trị của phần tử
            this.datatable[this.seletedRowIndex] = newValue;
        },
        /*
         * Format giá trị tiền
         * Author: BATUAN (27/05/2023)
         */
        formatedMoney(value) {
            return formatMoney(value);
        },
    },
};
</script>

<style>
@import url(@/css/components/table.css);
</style>
