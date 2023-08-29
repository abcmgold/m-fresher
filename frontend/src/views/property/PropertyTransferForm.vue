<template>
    <div
        class="transfer-form"
        v-esc="
            () => {
                this.$emit('hideTransferForm');
            }
        "
    >
        <div class="transfer-form-header">
            <div class="transfer-form__title">Thêm chứng từ điều chuyển</div>
            <div
                class="icon--close"
                @click="
                    () => {
                        this.$emit('hideTransferForm');
                    }
                "
            ></div>
        </div>
        <div class="general-info">
            <div class="general-info__up" ref="generalInfoUp">
                <div class="general-info__title">Thông tin chung</div>
                <div class="general-info__content" ref="generalInfoContent">
                    <div class="row">
                        <div class="col-2">
                            <m-group-input text="Mã chứng từ" :isForce="true">
                                <m-text-input
                                    ref="transferAssetCodeInput"
                                    v-model="this.transferAsset.TransferAssetCode"
                                    placeholder="Nhập mã chứng từ"
                                    :isRequired="true"
                                    type="text-field"
                                ></m-text-input>
                            </m-group-input>
                        </div>
                        <div class="col-2">
                            <m-group-input text="Ngày chứng từ" :isForce="true"
                                ><m-date-picker
                                    v-model="this.transferAsset.TransactionDate"
                                    ref="transactionDateInput"
                                    type="date"
                                    :isRequired="true"
                                ></m-date-picker>
                            </m-group-input>
                        </div>
                        <div class="col-2">
                            <m-group-input text="Ngày điều chuyển" :isForce="true"
                                ><m-date-picker
                                    v-model="this.transferAsset.TransferDate"
                                    ref="transferDateInput"
                                    type="date"
                                    :isRequired="true"
                                ></m-date-picker>
                            </m-group-input>
                        </div>
                        <div class="col-6">
                            <m-group-input text="Ghi chú">
                                <m-text-input
                                    v-model="this.transferAsset.Note"
                                    ref="propertyNameInput"
                                    type="text-field"
                                ></m-text-input>
                            </m-group-input>
                        </div>
                    </div>
                    <div class="general-info-delivery">
                        <div class="delivery-checkbox">
                            <m-checkbox ref="checkbox-delivery" @click="handleMoreDeliveryOptions"></m-checkbox>
                            <div>Chọn ban giao nhận</div>
                        </div>
                        <div class="delivery-checkbox" v-if="this.isShowMoreDeliveryOptions">
                            <m-checkbox></m-checkbox>
                            <div>Thêm ban giao nhận từ lần nhập trước</div>
                        </div>
                    </div>
                    <div class="delivery-detail" v-if="this.isShowMoreDeliveryOptions">
                        <div class="delivery-detail__header">
                            <div style="min-width: 36px">STT</div>
                            <div style="width: 25%">Họ và tên</div>
                            <div style="width: 25%">Đại diện</div>
                            <div style="width: 25%">Chức vụ</div>
                            <div style="width: calc(25% - 36px)"></div>
                        </div>
                        <div class="delivery-detail__body">
                            <div v-for="(user, index) in listDeliveryUsers" :key="index" class="delivery-detail__row">
                                <div class="delivery__order" style="min-width: 36px; height: 36px">
                                    {{ user.order }}
                                </div>
                                <div style="width: 25%">
                                    <m-text-input v-model="user.fullName"></m-text-input>
                                </div>
                                <div style="width: 25%"><m-text-input v-model="user.represent"></m-text-input></div>
                                <div style="width: 25%"><m-text-input v-model="user.position"></m-text-input></div>
                                <div class="actions--btn" style="width: calc(25% - 36px)">
                                    <div
                                        class="icon--up--v2"
                                        v-if="this.listDeliveryUsers.length != 1"
                                        @click="pushUpDeliveryInput(index)"
                                    ></div>
                                    <div
                                        class="icon--down--v2"
                                        v-if="this.listDeliveryUsers.length != 1"
                                        @click="pushDownDeliveryInput(index)"
                                    ></div>
                                    <div class="icon--add-v2" @click="addDeliveryInput(index)"></div>
                                    <div
                                        class="icon--delete"
                                        v-if="index != 0"
                                        @click="deleteDeliveryInput(index)"
                                    ></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="general-info__down">
                <div class="general-navbar">
                    <div class="general-navbar__left">
                        <div class="general-navbar__title">Thông tin tài sản điều chuyển</div>
                        <div class="genral-navbar__input">
                            <m-text-input
                                v-model="this.searchInput"
                                :placeholder="this.$_MISAResource['vn-VI'].searchProperty"
                                individualClass="text__input-icon-start"
                            ></m-text-input>
                        </div>
                    </div>
                    <div class="general-navbar__right">
                        <m-button
                            label="Chọn tài sản"
                            class=""
                            :individualClass="'btn btn--sub'"
                            :icon="'icon--add-v2'"
                            @click="showChoosenForm"
                        ></m-button>
                    </div>
                </div>
                <div class="general-navbar__table">
                    <div class="table__content">
                        <div class="table__content--header">
                            <div
                                class="table__content--header-item cell--item"
                                v-for="(header, index) in this.listHeaderUpdateTransfer"
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
                                v-for="(data, index) in this.propertyTransferListPaging"
                                :key="data.Id"
                                class="table__content--row"
                            >
                                <div class="text-align-center cell--item cell--item--checkbox" style="width: 50px">
                                    <m-checkbox
                                        :ref="`checkbox-${data.PropertyId}`"
                                        :id="data.PropertyId"
                                        @click="clickOnCheckbox(index, data.PropertyId)"
                                    ></m-checkbox>
                                </div>
                                <div class="text-align-center cell--item" style="width: 50px">{{ index + 1 }}</div>
                                <div class="text-align-left cell--item" style="width: 150px">
                                    {{ data.PropertyCode }}
                                </div>
                                <div class="text-align-left cell--item" style="width: 150px">
                                    {{ data.PropertyName }}
                                </div>
                                <div class="text-align-right cell--item" style="width: 150px">
                                    {{ this.formatedMoney(data.OriginalPrice) }}
                                </div>
                                <div class="text-align-right cell--item" style="width: 150px">
                                    {{ this.formatedMoney(caculateResidualPrice(data)) }}
                                </div>
                                <div class="text-align-left cell--item" style="width: 180px">
                                    {{ data.DepartmentName }}
                                </div>
                                <div class="text-align-right cell--item" style="width: 220px">
                                    <m-combo-box
                                        ref=""
                                        :isRequired="true"
                                        type="combo-box"
                                        :filterable="true"
                                        v-model="data.DepartmentTransferName"
                                        :dataSelect="this.departmentNames"
                                    ></m-combo-box>
                                </div>
                                <div class="text-align-left cell--item" style="flex: 1">
                                    <m-text-input ref="" v-model="data.Reason" type="text-field"></m-text-input>
                                </div>
                                <div class="table-list-icons cell--item" style="width: 100px">
                                    <div
                                        v-tippy="$_MISAResource['vn-VI'].delete"
                                        @click.stop
                                        class="table--icon table--icon-delete"
                                        @click="this.showDuplicate(index, data.id)"
                                    ></div>
                                </div>
                            </div>
                        </div>
                        <div class="table__content--sumary" v-if="this.$store.getters.getIsShowSummary"></div>
                        <div class="table__paging paging" v-if="this.$store.getters.getIsShowPaging">
                            <m-pagination
                                :dataSelect="this.numberOfRecordsPerPage"
                                :numberPages="Math.ceil(this.propertyTransferList.length / pageSize) * 10"
                                @changeCurrentPage="changeCurrentPage"
                                :pageSize="this.pageSize"
                                :totalRecords="this.propertyTransferList.length"
                                @changePageSize="changePageSize"
                            ></m-pagination>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="general-info__actions">
            <m-button
                individualClass="btn--primary"
                :label="this.$_MISAResource['vn-VI'].save"
                @click="createNewTransferAsset"
            ></m-button>
            <m-button
                individualClass="btn--noborder"
                :label="this.$_MISAResource['vn-VI'].cancel"
                ref="lastButton"
            ></m-button>
        </div>
    </div>
    <ChoosenForm
        v-if="isShowChoosenForm"
        @hideChoosenForm="this.hideChoosenForm"
        @updateListSelectedProperty="this.updateListSelectedProperty"
        :listTransferId="this.listTransferId"
    ></ChoosenForm>
    <m-modal v-if="this.isShowDialog">
        <m-dialog
            :type="this.typeDialog"
            :text="this.textDialog"
            :status="this.statusDialog"
            :documentInfo="this.documentInfoDialog"
            :dialogActions="this.dialogActions"
        ></m-dialog>
    </m-modal>
</template>

<script scoped>
import request from '@/common/api';
import ChoosenForm from './ChoosenForm.vue';
import exception from '@/common/exception';
import { formatMoney } from '@/common/common';
export default {
    name: 'PropertyTransferFrom',
    components: {
        ChoosenForm,
    },
    emits: ['hideTransferForm'],
    data() {
        return {
            listHeaderUpdateTransfer: this.$_MISAResource['vn-VI'].listHeaderUpdateTranfer,
            listTransferId: [],
            propertyTransferList: [],
            isShowChoosenForm: false,
            isShowMoreDeliveryOptions: false,
            listDeliveryUsers: [],
            pageNumber: 1,
            pageSize: '5',
            currentPage: 1,
            numberOfRecordsPerPage: [
                {
                    label: '5',
                    value: '5',
                },
                {
                    label: '10',
                    value: '10',
                },
                {
                    label: '20',
                    value: '20',
                },
            ],
            propertyTransferListPaging: [],
            selectedRow: [],
            searchInput: '',
            transferAsset: {},
            departmentNames: [],
            isShowDialog: false,
            textDialog: '',
            documentInfoDialog: [],
            dialogActions: {},
        };
    },
    async created() {
        this.paging();

        await this.getAllDepartments();

        this.generateDefauleValue();
    },
    methods: {
        /*
         * khởi tạo một số giá trị mặc định
         * Author: BATUAN (07/06/2023)
         */
        generateDefauleValue() {
            this.transferAsset.TransactionDate = new Date();
            this.transferAsset.TransferDate = new Date();
        },
        hideChoosenForm() {
            this.isShowChoosenForm = false;
        },
        showChoosenForm() {
            this.isShowChoosenForm = true;
        },
        updateListSelectedProperty(newPropertyTransferList) {
            this.propertyTransferList = this.propertyTransferList.concat(newPropertyTransferList);
            this.paging();
        },
        handleMoreDeliveryOptions() {
            if (this.$refs['checkbox-delivery'].isChecked) {
                this.$refs['checkbox-delivery'].isChecked = false;
                this.isShowMoreDeliveryOptions = false;
                this.listDeliveryUsers = [];
            } else {
                this.$refs['checkbox-delivery'].isChecked = true;
                this.isShowMoreDeliveryOptions = true;
                this.listDeliveryUsers.push({
                    order: 1,
                });
            }
        },
        addDeliveryInput(index) {
            for (let i = 0; i < this.listDeliveryUsers.length; i++) {
                if (this.listDeliveryUsers[i].order > index + 1) {
                    this.listDeliveryUsers[i].order++;
                }
            }
            this.listDeliveryUsers.push({
                order: index + 2,
            });
            this.listDeliveryUsers.sort((a, b) => a.order - b.order);
        },
        deleteDeliveryInput(index) {
            this.listDeliveryUsers = this.listDeliveryUsers.filter((element) => {
                return element.order != index + 1;
            });
            for (let i = 0; i < this.listDeliveryUsers.length; i++) {
                if (this.listDeliveryUsers[i].order > index + 1) {
                    this.listDeliveryUsers[i].order--;
                }
            }
        },
        pushUpDeliveryInput(index) {
            if (index != 0) {
                let tmp;
                tmp = this.listDeliveryUsers[index - 1].order;
                this.listDeliveryUsers[index - 1].order = this.listDeliveryUsers[index].order;
                this.listDeliveryUsers[index].order = tmp;
            }
            this.listDeliveryUsers.sort((a, b) => a.order - b.order);
        },
        pushDownDeliveryInput(index) {
            if (index < this.listDeliveryUsers.length - 1) {
                let tmp;
                tmp = this.listDeliveryUsers[index + 1].order;
                this.listDeliveryUsers[index + 1].order = this.listDeliveryUsers[index].order;
                this.listDeliveryUsers[index].order = tmp;
            }
            this.listDeliveryUsers.sort((a, b) => a.order - b.order);
        },

        paging() {
            let beginIndex = (this.currentPage - 1) * this.pageSize;
            let endIndex = beginIndex + Number(this.pageSize);
            console.log(beginIndex, endIndex);
            this.propertyTransferListPaging = this.propertyTransferList.slice(beginIndex, endIndex);
        },

        /*
         * Sự kiện khi thay đổi giá trị của currentPage khi click sang trái hoặc phải ở phần paging
         * Author: BATUAN (07/06/2023)
         */
        changeCurrentPage(newPage) {
            this.currentPage = newPage;
            this.paging();
        },
        /*
         * Sự kiện khi thay đổi giá trị của pageSize
         * Author: BATUAN (07/06/2023)
         */
        changePageSize(newValue) {
            this.pageSize = newValue;
            this.paging();
        },
        clickOnCheckBoxAll() {
            if (!this.$refs['checkbox-all'][0].isChecked) {
                this.$refs['checkbox-all'][0].isChecked = true;
                for (let i = 0; i < this.propertyTransferListPaging.length; i++) {
                    if (
                        !this.selectedRow.some(
                            (item) => item.PropertyId == this.propertyTransferListPaging[i].PropertyId,
                        )
                    ) {
                        this.selectedRow.push(this.propertyTransferListPaging[i]);
                    }
                    this.checkForCheckbox();
                }
            } else if (this.$refs['checkbox-all'][0].isChecked) {
                this.$refs['checkbox-all'][0].isChecked = false;
                let listPropertyId = [];
                this.propertyTransferListPaging.forEach((element) => {
                    listPropertyId.push(element.PropertyId);
                });

                this.selectedRow = this.selectedRow.filter((item) => {
                    return !listPropertyId.includes(item.PropertyId);
                });
                this.checkForCheckbox();
            }
        },
        clickOnCheckbox(index, id) {
            console.log(index);
            const checkboxRef = this.$refs[`checkbox-${id}`][0];
            if (checkboxRef) {
                if (checkboxRef.isChecked) {
                    this.selectedRow = this.selectedRow.filter((item) => {
                        return item.PropertyId !== id;
                    });
                } else {
                    this.selectedRow.push(this.propertyTransferListPaging[index]);
                }
                this.checkForCheckbox();
                this.checkFullChecked();
            }
        },
        /*
         * Kiểm tra để thực hiện check các ô checkbox đang được checked
         * Author: BATUAN (28/06/2023)
         */
        checkForCheckbox() {
            for (let i = 0; i < this.propertyTransferListPaging.length; i++) {
                if (this.selectedRow.some((item) => item.PropertyId == this.propertyTransferListPaging[i].PropertyId)) {
                    this.$refs[`checkbox-${this.propertyTransferListPaging[i].PropertyId}`][0].isChecked = true;
                } else {
                    this.$refs[`checkbox-${this.propertyTransferListPaging[i].PropertyId}`][0].isChecked = false;
                }
            }
        },
        /*
         * Kiểm tra xem tất cả các hàng trong 1 trang table có được chọn hay không
         * Author: BATUAN (28/06/2023)
         */
        checkFullChecked() {
            let check = true;
            this.propertyTransferListPaging.forEach((data) => {
                if (!this.selectedRow.some((item) => item.PropertyId == data.PropertyId)) {
                    check = false;
                }
            });

            if (check == true && this.propertyTransferListPaging.length > 0) {
                this.$refs['checkbox-all'][0].isChecked = true;
            } else {
                this.$refs['checkbox-all'][0].isChecked = false;
            }
        },
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
                if (!this.selectedRow.includes(id)) {
                    this.selectedRow.push(this.propertyTransferListPaging[index]);
                } else {
                    this.selectedRow = this.selectedRow.filter((row) => {
                        return row.PropertyId != id;
                    });
                }
            } else if (event.shiftKey) {
                if (this.firstClickRow || this.firstClickRow === 0) {
                    this.secondClickRow = index;
                    if (this.secondClickRow > this.firstClickRow) {
                        for (let i = this.firstClickRow; i <= this.secondClickRow; i++) {
                            const property = this.propertyTransferListPaging[i];
                            if (!this.selectedRow.some((item) => item.PropertyId == property.PropertyId)) {
                                this.selectedRow.push(property);
                            }
                            // if (!this.selectedRow.includes(property)) {
                            // }
                        }
                    } else {
                        for (let i = this.secondClickRow; i <= this.firstClickRow; i++) {
                            const property = this.propertyTransferListPaging[i];
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
         * Gọi Api thêm mới chứng từ điều chuyển
         * Author: BATUAN (29/06/2023)
         */
        async createNewTransferAsset() {
            if (this.validateData()) {
                this.transferAsset.TransferAssetDetailCreateList = [...this.propertyTransferList];
                this.transferAsset.OriginalPrice = 0;
                for (let i = 0; i < this.propertyTransferList.length; i++) {
                    this.transferAsset.OriginalPrice += this.propertyTransferList[i].OriginalPrice;
                }
                await request
                    .insertRecord('TransferAsset/Insert', this.transferAsset)
                    .then((res) => console.log(res))
                    .catch((err) => console.log(err));
            }
        },
        /*
         * Gọi Api lấy danh sách các phòng ban
         * Author: BATUAN (29/06/2023)
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
                    this.handleException(err.statusCode, err.message, '', this.showDialog);
                });
        },
        /*
         * validate dữ liệu của chứng từ trước khi gửi lên server
         * Author: BATUAN (29/06/2023)
         */
        validateData() {
            let textDialog = '';
            let check = true;
            let refErrorName = ''
            if (this.transferAsset.TransferAssetCode == undefined || this.transferAsset.TransferAssetCode.trim().length == 0) {
                textDialog += 'Mã chứng từ không được phép trống !</br>';
                check = false;
                refErrorName = 'transferAssetCodeInput'
            }
            if (!this.transferAsset.TransferDate) {
                textDialog += 'Ngày điều chuyển không được phép trống !<br>';
                refErrorName = refErrorName ? refErrorName : 'transferDateInput'

            }
            if (!this.transferAsset.TransactionDate) {
                textDialog += 'Ngày chứng từ không được phép trống !<br>';
                refErrorName = refErrorName ? refErrorName : 'transactionDateInput'
            }
            this.showDialog(textDialog, {
                thirdDialogBtnText: 'Đóng',
                thirdBtnFunction: () => {
                    this.hideDialog()
                    this.focusOnErrorInput(refErrorName)
                },
            },);
            return check;
        },
        /*
         * focus vào ô input lỗi nếu có
         * Author: BATUAN (29/06/2023)
         */
        focusOnErrorInput(errorField) {
            this.$refs[errorField].autoFocus();
        },
        /*
         * Hiển thị dialog
         * Author: BATUAN (29/08/2023)
         */
        showDialog(
            textDialog,
            dialogActions = {
                thirdDialogBtnText: 'Đóng',
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
        /*
         * Xử lý mã lỗi backend trả về
         * Author: BATUAN (29/06/2023)
         */
        handleException(statusCode, message, documentInfo, showToastError) {
            exception(statusCode, message, documentInfo, showToastError);
        },
        /*
         * Format giá trị tiền
         * Author: BATUAN (27/05/2023)
         */
        formatedMoney(value) {
            return formatMoney(value);
        },
        /*
         * Tính toán giá trị còn lại của các tài sản
         * Author: BATUAN (30/08/2023)
         */
        caculateResidualPrice(property) {
            const currentDate = new Date();
            return property.OriginalPrice - (currentDate.getFullYear() - property.FollowYear) * property.WearRateValue;
        },
    },
};
</script>

<style scoped>
@import url(../../css/views/property/property-transfer-form.css);
@import url(../../css/components/icon.css);
@import url(../../css/components/button.css);
</style>
