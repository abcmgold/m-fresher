<template>
    <m-modal
        @keydown.esc="
            () => {
                this.$emit('hideChoosenForm');
            }
        "
        tabindex="0"
        ref="modal"
    >
        <div class="choosen-from">
            <div class="choosen-form-header">
                <div class="choosen-form-header--left">
                    <div class="choosen-form__title">
                        {{ this.$_MISAResource['vn-VI'].choosenForm.choosePropertyTransfer }}
                    </div>
                    <m-text-input
                        v-model="this.inputSearch"
                        :placeholder="this.$_MISAResource['vn-VI'].searchProperty"
                        individualClass="text__input-icon-start"
                        ref="inputSearch"
                    ></m-text-input>
                </div>
                <div
                    class="icon--close"
                    @click="
                        () => {
                            this.$emit('hideChoosenForm');
                        }
                    "
                    :content="this.$_MISAResource['vn-VI'].close"
                    v-tippy="{ placement: 'top' }"
                ></div>
            </div>
            <div class="choosen-form__table">
                <div class="table__content">
                    <div class="table__content--header">
                        <div
                            class="table__content--header-item cell--item"
                            v-for="(header, index) in this.listHeader"
                            :class="{ 'cell--item--checkbox': header.name == 'checkbox' }"
                            :key="index"
                            :style="header.style"
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
                    <div class="table__content--body">
                        <div
                            v-for="(data, index) in this.listProperty"
                            :key="data.PropertyId"
                            class="table__content--row"
                            :class="{
                                'row-selected': isSelected(data),
                                'row-only-selected': this.clickIndex == index,
                            }"
                            @click="clickOnRowTable(index, data.PropertyId, $event)"
                        >
                            <div class="text-align-center cell--item cell--item--checkbox" style="width: 50px">
                                <m-checkbox
                                    @event.stop
                                    :ref="`checkbox-${data.PropertyId}`"
                                    @click="clickOnCheckbox(index, data.PropertyId)"
                                    name="id"
                                ></m-checkbox>
                            </div>
                            <div class="text-align-center cell--item" style="width: 50px">{{ index + 1 }}</div>
                            <div class="text-align-left cell--item" style="width: 150px">
                                {{ data.PropertyCode }}
                            </div>
                            <div class="text-align-left cell--item" style="width: 200px">
                                {{ data.PropertyName }}
                            </div>
                            <div class="text-align-left cell--item" style="width: 200px">
                                {{ data.DepartmentTransferName ? data.DepartmentTransferName : data.DepartmentName }}
                            </div>
                            <div class="text-align-right cell--item" style="width: 150px">
                                {{ this.formatedMoney(data.OriginalPrice) }}
                            </div>
                            <div class="text-align-right cell--item" style="width: 150px">
                                {{ this.formatedMoney(data.ResidualPrice) }}
                            </div>
                            <div class="text-align-right cell--item" style="width: 150px">
                                {{ data.FollowYear }}
                            </div>
                        </div>
                    </div>
                    <div class="table__content--sumary" v-if="this.$store.getters.getIsShowSummary">
                        <div style="width: 50px"></div>
                        <div style="width: 50px"></div>
                        <div style="width: 150px"></div>
                        <div style="width: 200px"></div>
                        <div style="width: 200px"></div>
                        <div class="text-align-right cell--item" style="width: 150px; font-weight: bold">
                            {{ this.formatedMoney(this.totalOriginalPrice) }}
                        </div>
                        <div class="text-align-right cell--item" style="width: 150px; font-weight: bold">
                            {{ this.formatedMoney(this.totalResidualPrice) }}
                        </div>
                        <div style="width: 150px"></div>
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
                    <div v-if="this.listProperty.length == 0" class="table__content--empty">
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
            </div>
            <div class="choosen-input">
                <div class="row">
                    <div class="col-4">
                        <m-group-input :text="this.$_MISAResource['vn-VI'].choosenForm.newDepartment" :isForce="true">
                            <m-combo-box
                                :dataSelect="this.departmentNames"
                                ref=""
                                :isRequired="true"
                                :placeholder="this.$_MISAResource['vn-VI'].choosenForm.chooseNewDepartment"
                                type="combo-box"
                                :filterable="true"
                                v-model="this.newDepartmentName"
                            ></m-combo-box>
                        </m-group-input>
                    </div>
                    <div class="col-8">
                        <m-group-input :text="this.$_MISAResource['vn-VI'].choosenForm.note">
                            <m-text-input
                                ref=""
                                :isRequired="false"
                                type="text-field"
                                v-model="this.note"
                            ></m-text-input>
                        </m-group-input>
                    </div>
                </div>
            </div>
            <div class="choosen-actions">
                <div class="choosen-action__left">
                    {{ this.$_MISAResource['vn-VI'].choosenForm.selected }}&nbsp;
                    <strong>{{ this.selectedRow.length }}</strong>
                    <m-button
                        individualClass="btn--noborder"
                        :label="this.$_MISAResource['vn-VI'].choosenForm.unselected"
                        @click="removeAllSelected"
                    ></m-button>
                </div>

                <div class="choosen-action__right">
                    <m-button
                        :label="this.$_MISAResource['vn-VI'].cancel"
                        individualClass="btn--sub"
                        @click="
                            () => {
                                this.$emit('hideChoosenForm');
                            }
                        "
                    >
                    </m-button>
                    <m-button
                        :label="this.$_MISAResource['vn-VI'].yes"
                        individualClass="btn--primary"
                        @click="handleChoosenProperty"
                    >
                    </m-button>
                </div>
            </div>
        </div>
    </m-modal>
    <m-modal v-if="this.isShowDialog">
        <m-dialog :type="this.typeDialog" :text="this.textDialog" :dialogActions="this.dialogActions"></m-dialog>
    </m-modal>
</template>

<script scoped>
import request from '@/common/api';
import { formatMoney } from '@/common/common';
import { delay } from '@/common/common';
import debounce from 'lodash/debounce';
export default {
    name: 'ChoosenForm',
    props: {
        excludedIds: Array,
        listTemporaryDelete: Array,
    },
    emits: ['hideChoosenForm', 'updateListSelectedProperty'],
    data() {
        return {
            listHeader: this.$_MISAResource['vn-VI'].listChoosenTranfer,
            currentPage: 1,
            pageSize: '20',
            listProperty: [],
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
            totalRecords: 0,
            pageNumber: 0,
            departmentNames: [],
            firstClickRow: 0,
            clickIndex: -1,
            selectedRow: [],
            newDepartmentName: '',
            newDepartmentId: '',
            note: '',
            isLoadingData: false,
            isShowDialog: false,
            textDialog: '',
            dialogActions: {},
            totalOriginalPrice: 0,
            totalResidualPrice: 0,
            inputSearch: '',
        };
    },
    async created() {
        await this.getListProperty();

        await this.getAllDepartments();
    },
    mounted() {
        this.$refs.inputSearch.$el.focus();
    },
    unmounted() {
        if (this.$parent) {
            this.$parent.$refs.transferAssetCodeInput.$el.focus();
        }
    },
    watch: {
        pageSize: function (newValue) {
            this.pageSize = newValue;

            this.currentPage = 1;

            this.pageNumber = Math.ceil(this.totalRecords / this.pageSize);
            this.selectedRow = [];
            this.checkForCheckbox();

            this.checkFullChecked();
            this.getListProperty();
        },
        currentPage: async function () {
            this.$refs['checkbox-all'][0].isChecked = false;
            await this.getListProperty();
            for (let i = 0; i < this.listProperty.length; i++) {
                if (this.$refs.checkbox && this.$refs.checkbox[i]) {
                    this.$refs.checkbox[i].isChecked = false;
                }
            }

            this.checkForCheckbox();

            this.checkFullChecked();
        },
        newDepartmentName: function (newValue) {
            for (let i = 0; i < this.departmentNames.length; i++) {
                if (this.departmentNames[i].label == newValue) {
                    this.newDepartmentId = this.departmentNames[i].value;
                }
            }
        },
        inputSearch: debounce(async function () {
            await this.getListProperty();
            this.checkForCheckbox();

            this.checkFullChecked();
        }, 1000),
    },
    methods: {
        /*
         * Gọi api lấy danh sách các tài sản
         * Author: BATUAN (07/06/2023)
         */
        async getListProperty() {
            this.isLoadingData = true;
            let tringId = this.excludedIds.join(', ');
            await request
                .getRecord(
                    `Property/CurrentInfo?pageNumber=${this.currentPage}&pageSize=${Number(
                        this.pageSize,
                    )}&searchInput=${this.inputSearch}&excludedIds=${tringId}`,
                )
                .then(async (res) => {
                    console.log(res);
                    await delay(500);
                    this.isLoadingData = false;
                    this.listProperty = res.data;
                    if (res.data.length > 0) {
                        this.totalRecords = res.data[0].TotalRecordCount;
                        this.totalOriginalPrice = res.data[0].TotalPrice;
                        this.totalResidualPrice = res.data[0].TotalResidualPrice;
                        this.pageNumber = Math.ceil(this.totalRecords / this.pageSize);
                    } else {
                        this.totalRecords = 0;
                        this.totalOriginalPrice = 0;
                        this.totalResidualPrice = 0;
                        this.pageNumber = 1;
                    }

                    if (this.listTemporaryDelete && this.listTemporaryDelete.length > 0) {
                        for (let i = 0; i < this.listProperty.length; i++) {
                            for (let j = 0; j < this.listTemporaryDelete.length; j++) {
                                if (this.listTemporaryDelete[j].PropertyId == this.listProperty[i].PropertyId) {
                                    this.listProperty[i].DepartmentTransferId =
                                        this.listTemporaryDelete[j].DepartmentId;
                                    this.listProperty[i].DepartmentTransferName =
                                        this.listTemporaryDelete[j].DepartmentName;
                                    break;
                                }
                            }
                        }
                    }
                })
                .catch((err) => {
                    console.log(err);
                });
        },
        /*
         * Gọi api lấy danh sách phòng ban
         * Author: BATUAN (07/06/2023)
         */
        async getAllDepartments() {
            await request
                .getRecord('Department')
                .then((response) => {
                    response.data.forEach((department) => {
                        this.departmentNames.push({
                            // id: department.DepartmentId,
                            value: department.DepartmentId,
                            label: department.DepartmentName,
                        });
                    });
                })
                .catch((err) => {
                    this.handleException(err.statusCode, err.message, this.showDialog);
                });
        },
        /*
         * Sự kiện click vào ô checkbox
         * Author: BATUAN (07/06/2023)
         */
        handleClickOnRow(index) {
            this.firstClickRow = index;
            this.clickIndex = index;
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
                if (!this.selectedRow.some((prop) => prop.PropertyId == id)) {
                    this.selectedRow.push(this.listProperty[index]);
                } else {
                    this.selectedRow = this.selectedRow.filter((row) => {
                        return row.PropertyId != id;
                    });
                }
            } else if (event.shiftKey) {
                if (this.firstClickRow || this.firstClickRow === 0) {
                    this.secondClickRow = index;

                    this.selectedRow = this.selectedRow.filter((prop) => {
                        return !this.listProperty.some((item) => item.PropertyId == prop.PropertyId);
                    });

                    if (this.secondClickRow > this.firstClickRow) {
                        for (let i = this.firstClickRow; i <= this.secondClickRow; i++) {
                            const property = this.listProperty[i];
                            if (!this.selectedRow.some((item) => item.PropertyId == property.PropertyId)) {
                                this.selectedRow.push(property);
                            }
                        }
                    } else {
                        for (let i = this.secondClickRow; i <= this.firstClickRow; i++) {
                            const property = this.listProperty[i];
                            if (!this.selectedRow.some((item) => item.PropertyId == property.PropertyId)) {
                                this.selectedRow.push(property);
                            }
                        }
                    }
                }
            } else {
                this.handleClickOnRow(index, id);
            }
            this.checkForCheckbox();
            this.checkFullChecked();
        },
        /*
         * Kiểm tra 1 hàng có đang được chọn hay không
         * Author: BATUAN (14/06/2023)
         */
        isSelected(property) {
            if (this.selectedRow) {
                return this.selectedRow.some((prop) => prop.PropertyId == property.PropertyId);
            }
            return false;
        },

        /*
         * Kiểm tra xem tất cả các hàng trong 1 trang table có được chọn hay không
         * Author: BATUAN (28/06/2023)
         */
        checkFullChecked() {
            let check = true;
            this.listProperty.forEach((data) => {
                if (!this.selectedRow.some((item) => item.PropertyId == data.PropertyId)) {
                    check = false;
                }
            });

            if (check == true && this.listProperty.length > 0) {
                this.$refs['checkbox-all'][0].isChecked = true;
            } else {
                this.$refs['checkbox-all'][0].isChecked = false;
            }
        },
        /*
         * Kiểm tra để thực hiện check các ô checkbox đang được checked
         * Author: BATUAN (28/06/2023)
         */
        checkForCheckbox() {
            for (let i = 0; i < this.listProperty.length; i++) {
                if (this.selectedRow.some((item) => item.PropertyId == this.listProperty[i].PropertyId)) {
                    this.$refs[`checkbox-${this.listProperty[i].PropertyId}`][0].isChecked = true;
                } else {
                    this.$refs[`checkbox-${this.listProperty[i].PropertyId}`][0].isChecked = false;
                }
            }
        },
        /*
         * Sự kiện click vào ô checkbox
         * Author: BATUAN (07/06/2023)
         */
        clickOnCheckbox(index, id) {
            const checkboxRef = this.$refs[`checkbox-${id}`][0];
            if (checkboxRef) {
                if (checkboxRef.isChecked) {
                    this.selectedRow = this.selectedRow.filter((item) => {
                        return item.PropertyId !== id;
                    });
                } else {
                    this.selectedRow.push(this.listProperty[index]);
                }
                this.checkForCheckbox();
                this.checkFullChecked();
            }
        },
        /*
         * Sự kiện click vào ô checkbox all
         * Author: BATUAN (07/06/2023)
         */
        clickOnCheckBoxAll() {
            if (!this.$refs['checkbox-all'][0].isChecked) {
                this.$refs['checkbox-all'][0].isChecked = true;
                for (let i = 0; i < this.listProperty.length; i++) {
                    if (!this.selectedRow.some((item) => item.PropertyId == this.listProperty[i].PropertyId)) {
                        this.selectedRow.push(this.listProperty[i]);
                    }
                    this.checkForCheckbox();
                }
            } else if (this.$refs['checkbox-all'][0].isChecked) {
                this.$refs['checkbox-all'][0].isChecked = false;
                let listPropertyId = [];
                this.listProperty.forEach((element) => {
                    listPropertyId.push(element.PropertyId);
                });

                this.selectedRow = this.selectedRow.filter((item) => {
                    return !listPropertyId.includes(item.PropertyId);
                });
                this.checkForCheckbox();
            }
        },
        /*
         * Loại bỏ hết các hàng đang chọn
         * Author: BATUAN (07/06/2023)
         */
        removeAllSelected() {
            this.selectedRow = [];
            this.checkForCheckbox();
            this.checkFullChecked();
        },
        /*
         * Sự kiện khi cập nhật danh sách tài sản
         * Author: BATUAN (07/06/2023)
         */
        handleChoosenProperty() {
            if (this.selectedRow.length == 0) {
                this.showDialog(this.$_MISAResource['vn-VI'].choosenForm.pleaseChooseTransferAsset);
            } else if (!this.newDepartmentName) {
                this.showDialog(this.$_MISAResource['vn-VI'].choosenForm.pleaseChooseNewDepartment);
            } else {
                let listSelectedProperty = [];
                let checked = true;

                for (let i = 0; i < this.selectedRow.length; i++) {
                    if (this.selectedRow[i].DepartmentTransferName == this.newDepartmentName) {
                        checked = false;
                        break;
                    }
                }
                if (checked == false) {
                    this.showDialog('Bộ phận sử dụng mới phải khác bộ phận cũ');
                } else {
                    for (let i = 0; i < this.selectedRow.length; i++) {
                        let property = { ...this.selectedRow[i] };
                        if (property.DepartmentTransferId === '00000000-0000-0000-0000-000000000000') {
                            property.DepartmentTransferId = property.DepartmentId;
                            property.DepartmentTransferName = property.DepartmentName;
                        }
                        property.Reason = this.note;
                        listSelectedProperty.push({
                            DepartmentId: property.DepartmentTransferId,
                            DepartmentName: property.DepartmentTransferName,
                            DepartmentTransferId: this.newDepartmentId,
                            DepartmentTransferName: this.newDepartmentName,
                            PropertyId: property.PropertyId,
                            OriginalPrice: property.OriginalPrice,
                            Reason: property.Reason,
                            PropertyCode: property.PropertyCode,
                            PropertyName: property.PropertyName,
                            FollowYear: property.FollowYear,
                            WearRateValue: property.WearRateValue,
                            ResidualPrice: property.ResidualPrice,
                        });
                    }
                    this.$emit('updateListSelectedProperty', listSelectedProperty);
                    this.$emit('hideChoosenForm');
                }
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
         * Sự kiện khi thay đổi giá trị của currentPage khi click sang trái hoặc phải ở phần paging
         * Author: BATUAN (07/06/2023)
         */
        changeCurrentPage(newPage) {
            this.currentPage = newPage;
            this.$refs.checkbox = [];
        },
        /*
         * Sự kiện khi thay đổi giá trị pagesize
         * Author: BATUAN (07/06/2023)
         */
        changePageSize: function (newValue) {
            this.pageSize = newValue;
        },
        /*
         * Hiển thị dialog
         * Author: BATUAN (29/08/2023)
         */
        showDialog(
            textDialog,
            dialogActions = {
                thirdDialogBtnText: this.$_MISAResource['vn-VI'].close,
                thirdBtnFunction: this.hideDialog,
            },
        ) {
            this.isShowDialog = true;
            this.textDialog = textDialog;
            this.dialogActions = dialogActions;
        },
        /*
         * Đóng dialog
         * Author: BATUAN (29/08/2023)
         */
        hideDialog() {
            this.isShowDialog = false;
        },
    },
};
</script>

<style scoped>
@import url(../../css/views/property/choosen-form.css);
@import url(../../css/components/button.css);
@import url(../../css/components/table.css);
@import url(../../css/components/modal.css);
</style>
