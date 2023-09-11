<template>
    <div
        class="transfer-form"
        ref="container"
        tabindex="0"
        @keydown.esc="handleClickClose"
        @keydown="handleSearchByKeyBoard"
    >
        <div class="transfer-form-header">
            <div class="transfer-form__title">{{ this.title }}</div>
            <div
                class="icon--close"
                @click="handleClickClose"
                :content="this.$_MISAResource['vn-VI'].close"
                v-tippy="{ placement: 'top' }"
            ></div>
        </div>
        <div class="general-info">
            <div class="general-info__up" ref="generalInfoUp">
                <div class="general-info__title">
                    {{ this.$_MISAResource['vn-VI'].propertyTransferForm.generalInfo }}
                </div>
                <div class="general-info__content" ref="generalInfoContent">
                    <div class="row">
                        <div class="col-2">
                            <m-group-input
                                :text="this.$_MISAResource['vn-VI'].propertyTransferForm.transferAssetCode"
                                :isForce="true"
                            >
                                <m-text-input
                                    ref="transferAssetCodeInput"
                                    v-model="this.transferAsset.TransferAssetCode"
                                    :placeholder="
                                        this.$_MISAResource['vn-VI'].propertyTransferForm.typeTransferAssetCode
                                    "
                                    :isRequired="true"
                                    type="text-field"
                                    :maxLength="this.$_MISAEnum.maxLengthCode"
                                ></m-text-input>
                            </m-group-input>
                        </div>
                        <div class="col-2">
                            <m-group-input
                                :text="this.$_MISAResource['vn-VI'].propertyTransferForm.transactionDate"
                                :isForce="true"
                                ><m-date-picker
                                    v-model="this.transferAsset.TransactionDate"
                                    ref="transactionDateInput"
                                    type="date"
                                    :isRequired="true"
                                ></m-date-picker>
                            </m-group-input>
                        </div>
                        <div class="col-2">
                            <m-group-input
                                :text="this.$_MISAResource['vn-VI'].propertyTransferForm.transferDate"
                                :isForce="true"
                                ><m-date-picker
                                    v-model="this.transferAsset.TransferDate"
                                    ref="transferDateInput"
                                    type="date"
                                    :isRequired="true"
                                ></m-date-picker>
                            </m-group-input>
                        </div>
                        <div class="col-6">
                            <m-group-input :text="this.$_MISAResource['vn-VI'].propertyTransferForm.note">
                                <m-text-input
                                    v-model="this.transferAsset.Note"
                                    ref="propertyNameInput"
                                    type="text-field"
                                    :maxLength="this.$_MISAEnum.maxLengthText"
                                ></m-text-input>
                            </m-group-input>
                        </div>
                    </div>
                    <div class="general-info-delivery">
                        <div class="delivery-checkbox">
                            <m-checkbox
                                ref="checkbox-delivery"
                                :isCheckedProp="this.isCheckboxDeliveryChecked"
                                @click="handleMoreDeliveryOptions"
                            ></m-checkbox>
                            <div>{{ this.$_MISAResource['vn-VI'].propertyTransferForm.chooseReceiver }}</div>
                        </div>
                        <div class="delivery-checkbox" v-if="this.isShowMoreDeliveryOptions">
                            <m-checkbox ref="checkboxAddReceiver" @click="handleClickAddLastestReceiver"></m-checkbox>
                            <div>{{ this.$_MISAResource['vn-VI'].propertyTransferForm.addMoreReceiver }}</div>
                        </div>
                    </div>
                    <div class="delivery-detail" v-if="this.isShowMoreDeliveryOptions">
                        <div class="delivery-detail__header">
                            <div style="min-width: 36px">
                                {{ this.$_MISAResource['vn-VI'].propertyTransferForm.order }}
                            </div>
                            <div style="width: 25%">
                                {{ this.$_MISAResource['vn-VI'].propertyTransferForm.fullName }}
                            </div>
                            <div style="width: 25%">
                                {{ this.$_MISAResource['vn-VI'].propertyTransferForm.represent }}
                            </div>
                            <div style="width: 25%">
                                {{ this.$_MISAResource['vn-VI'].propertyTransferForm.position }}
                            </div>
                            <div style="width: calc(25% - 36px)"></div>
                        </div>
                        <div class="delivery-detail__body">
                            <div
                                v-for="(user, index) in this.transferAsset.ReceiverList"
                                :key="user.ReceiverId"
                                class="delivery-detail__row"
                            >
                                <div class="delivery__order" style="min-width: 36px; height: 36px">
                                    {{ index + 1 }}
                                </div>
                                <div style="width: 25%">
                                    <m-text-input
                                        v-model="user.FullName"
                                        :maxLength="this.$_MISAEnum.maxLengthName"
                                    ></m-text-input>
                                </div>
                                <div style="width: 25%">
                                    <m-text-input
                                        v-model="user.Represent"
                                        :maxLength="this.$_MISAEnum.maxLengthText"
                                    ></m-text-input>
                                </div>
                                <div style="width: 25%">
                                    <m-text-input
                                        v-model="user.Position"
                                        :maxLength="this.$_MISAEnum.maxLengthText"
                                    ></m-text-input>
                                </div>
                                <div class="actions--btn" style="width: calc(25% - 36px)">
                                    <div
                                        class="icon--up--v2"
                                        v-if="this.transferAsset.ReceiverList.length != 1"
                                        @click="pushUpDeliveryInput(index)"
                                    ></div>
                                    <div
                                        class="icon--down--v2"
                                        v-if="this.transferAsset.ReceiverList.length != 1"
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
                        <div class="general-navbar__title">
                            {{ this.$_MISAResource['vn-VI'].propertyTransferForm.infoTransferAsset }}
                        </div>
                        <div class="genral-navbar__input">
                            <m-text-input
                                v-model="this.inputFilter"
                                :placeholder="this.$_MISAResource['vn-VI'].searchProperty"
                                individualClass="text__input-icon-start"
                                ref="inputSearch"
                            ></m-text-input>
                        </div>
                    </div>
                    <div class="general-navbar__right">
                        <div class="number-select" v-if="this.selectedRow.length">
                            {{ this.$_MISAResource['vn-VI'].propertyTransferForm.selected }}
                            <strong>{{ this.selectedRow.length }}</strong>
                        </div>
                        <m-button
                            v-if="this.selectedRow.length"
                            individualClass="btn--noborder"
                            :label="this.$_MISAResource['vn-VI'].propertyTransferForm.unselected"
                            @click="removeAllSelected"
                        ></m-button>
                        <m-button
                            v-if="this.selectedRow.length"
                            class="btn--unnormal"
                            individualClass="btn--sub"
                            :label="this.$_MISAResource['vn-VI'].delete"
                            @click="checkDeleteRows"
                        ></m-button>
                        <m-button
                            :label="this.$_MISAResource['vn-VI'].propertyTransferForm.chooseProperty"
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
                        <div class="table__content--body" @scroll="handleScrollBodyTable">
                            <div
                                v-for="(data, index) in this.propertyTransferListPaging"
                                :key="data.TransferAssetDetailId"
                                class="table__content--row"
                                :class="{
                                    'row-selected': isSelected(data),
                                    'row-only-selected': this.clickIndex == index,
                                }"
                                @click="clickOnRowTable(index, data.PropertyId, $event)"
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
                                    <div class="text--surround">{{ data.PropertyCode }}</div>
                                </div>
                                <div class="text-align-left cell--item" style="width: 150px">
                                    <div class="text--surround">{{ data.PropertyName }}</div>
                                </div>
                                <div class="text-align-right cell--item" style="width: 150px">
                                    <div class="text--surround">{{ this.formatedMoney(data.OriginalPrice) }}</div>
                                </div>
                                <div class="text-align-right cell--item" style="width: 150px">
                                    <div class="text--surround">{{ this.formatedMoney(data.ResidualPrice) }}</div>
                                </div>
                                <div class="text-align-left cell--item" style="width: 180px">
                                    <div class="text--surround">{{ data.DepartmentName }}</div>
                                </div>
                                <div class="text-align-right cell--item" style="width: 220px">
                                    <m-combo-box
                                        ref="combobox"
                                        :isRequired="true"
                                        type="combo-box"
                                        :filterable="true"
                                        v-model="data.DepartmentTransferName"
                                        :dataSelect="this.departmentNames"
                                    ></m-combo-box>
                                </div>
                                <div class="text-align-left cell--item" style="flex: 1">
                                    <m-text-input
                                        ref=""
                                        v-model="data.Reason"
                                        type="text-field"
                                        :maxLength="this.$_MISAEnum.maxLengthText"
                                    ></m-text-input>
                                </div>
                                <div class="table-list-icons cell--item" style="width: 100px">
                                    <div
                                        v-tippy="$_MISAResource['vn-VI'].delete"
                                        @click.stop
                                        class="table--icon table--icon-delete"
                                        @click="this.checkDeleteRow(data)"
                                    ></div>
                                </div>
                            </div>
                        </div>
                        <div class="table__content--sumary">
                            <div style="width: 50px"></div>
                            <div style="width: 50px"></div>
                            <div style="width: 150px"></div>
                            <div style="width: 150px"></div>
                            <div class="text-align-right cell--item" style="width: 150px; font-weight: bold">
                                {{ this.formatedMoney(this.totalOriginalPrice) }}
                            </div>
                            <div class="text-align-right cell--item" style="width: 150px; font-weight: bold">
                                {{ this.formatedMoney(this.totalResidualPrice) }}
                            </div>
                            <div style="width: 180px"></div>
                            <div style="width: 220px"></div>
                            <div style="flex: 1"></div>
                            <div style="width: 100px"></div>
                        </div>
                        <div class="table__paging paging">
                            <m-pagination
                                :dataSelect="this.numberOfRecordsPerPage"
                                :numberPages="this.numberPages * 10"
                                @changeCurrentPage="changeCurrentPage"
                                :pageSize="this.pageSize"
                                :totalRecords="this.totalRecords"
                                @changePageSize="changePageSize"
                            ></m-pagination>
                        </div>
                        <div v-if="this.propertyTransferListPaging.length == 0" class="table__content--empty">
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
            </div>
        </div>
        <div class="general-info__actions">
            <m-button
                individualClass="btn--primary"
                :label="this.$_MISAResource['vn-VI'].save"
                @click="btnSaveAction"
            ></m-button>
            <m-button
                individualClass="btn--sub"
                :label="this.$_MISAResource['vn-VI'].cancel"
                ref="lastButton"
                @click="handleClickClose"
            ></m-button>
        </div>
    </div>
    <ChoosenForm
        v-if="isShowChoosenForm"
        @hideChoosenForm="this.hideChoosenForm"
        @updateListSelectedProperty="this.updateListSelectedProperty"
        :excludedIds="this.excludedIds"
        :listTemporaryDelete="this.listTemporaryDelete"
    ></ChoosenForm>
    <m-modal v-if="this.isShowDialog">
        <m-dialog
            :text="this.textDialog"
            :documentInfo="this.documentInfo"
            :dialogActions="this.dialogActions"
        ></m-dialog>
    </m-modal>
</template>

<script scoped>
import request from '@/common/api';
import ChoosenForm from './ChoosenForm.vue';
import exception from '@/common/exception';
import { formatMoney } from '@/common/common';
import debounce from 'lodash/debounce';
import { delay } from '@/common/common';

export default {
    name: 'PropertyTransferFrom',
    components: {
        ChoosenForm,
    },
    props: {
        formMode: Number,
        transferAssetProps: Object,
        transferAssetAutoCode: String,
    },
    emits: ['hideTransferForm', 'showToastSuccess', 'createdNewTransferAsset', 'updateTransferAsset'],
    data() {
        return {
            listHeaderUpdateTransfer: this.$_MISAResource['vn-VI'].listHeaderUpdateTranfer,
            propertyTransferList: [],
            isShowChoosenForm: false,
            isShowMoreDeliveryOptions: false,
            pageNumber: 1,
            pageSize: '10',
            currentPage: 1,
            numberOfRecordsPerPage: [
                {
                    label: '10',
                    value: '10',
                },
                {
                    label: '15',
                    value: '15',
                },
                {
                    label: '20',
                    value: '20',
                },
            ],
            propertyTransferListPaging: [],
            selectedRow: [],
            transferAsset: {},
            departmentNames: [],
            isShowDialog: false,
            textDialog: '',
            documentInfo: '',
            dialogActions: {},
            excludedIds: [],
            totalRecords: 0,
            transferAssetDetailAdd: [],
            transferAssetDetailDelete: [],
            transferAssetDetailUpdate: [],
            transferAssetDetailNochange: [],
            listInitialTransferAssetDetail: [],
            receiverAdd: [],
            receiverDelete: [],
            receiverUpdate: [],
            receiverNochange: [],
            listInitialReceiver: [],
            clickIndex: -1,
            inputFilter: '',
            isLoadingData: false,
            numberPages: 1,
            listReceiverLastest: [],
            totalResidualPrice: 0,
            totalOriginalPrice: 0,
            listTemporaryDelete: [], // lưu danh sách tài sản xóa mềm (tài sản điều chuyển đã nằm trong database)
            isCheckboxDeliveryChecked: false,
        };
    },
    watch: {
        inputFilter: debounce(function () {
            this.paging();
        }, 1000),
    },
    async created() {
        switch (this.formMode) {
            case this.$_MISAEnum.formUpdate:
                this.title = this.$_MISAResource['vn-VI'].propertyTransferForm.updateTransferAsset;
                this.transferAsset = JSON.parse(JSON.stringify(this.transferAssetProps));
                this.totalRecords = this.transferAsset.TransferAssetDetailList.length;
                this.calculateTotalResidualPrice();
                this.calculateTotalOriginalPrice();
                this.listInitialTransferAssetDetail = JSON.parse(
                    JSON.stringify(this.transferAsset.TransferAssetDetailList),
                );
                // this.listReceiver = JSON.parse(JSON.stringify(this.transferAsset.ReceiverList));
                this.listInitialReceiver = JSON.parse(JSON.stringify(this.transferAsset.ReceiverList));
                this.isCheckboxDeliveryChecked = this.transferAsset.ReceiverList.length > 0 ? true : false;
                this.isShowMoreDeliveryOptions = this.isCheckboxDeliveryChecked ? true : false;
                break;
            case this.$_MISAEnum.formAdd:
                this.title = this.$_MISAResource['vn-VI'].propertyTransferForm.insertTransferAsset;
                this.transferAsset.TransferAssetCode = this.transferAssetAutoCode;
                await this.generateDefaultValue();
                break;
            default:
                break;
        }

        this.paging();

        await this.getAllDepartments();

        this.clickIndex = 0;

        this.firstClickRow = this.clickIndex;
    },
    mounted() {
        this.$refs.transferAssetCodeInput.$el.focus();

        console.log(this.$refs['checkbox-delivery'].isChecked);
        // if (this.transferAsset.ReceiverList && this.transferAsset.ReceiverList.length > 0) {
        //     this.$refs['checkbox-delivery'].isChecked = true;
        //     this.listReceiver = JSON.parse(JSON.stringify(this.transferAsset.ReceiverList));
        //     console.log(this.listReceiver)
        // }
    },
    unmounted() {
        if (this.$parent) {
            this.$parent.$refs.container.focus();
        }
    },
    methods: {
        /*
         * Gọi api lấy danh sách người nhận mới nhất
         * Author: BATUAN (27/08/2023)
         */
        async getLastestReceiver() {
            await request
                .getRecord('Receiver')
                .then((response) => (this.listReceiverLastest = response.data))
                .catch((err) => {
                    this.handleException(err.statusCode, err.message, err.documentInfo, this.showDialog);
                });
        },
        /*
         * khởi tạo một số giá trị mặc định
         * Author: BATUAN (07/06/2023)
         */
        async generateDefaultValue() {
            this.transferAsset.TransactionDate = new Date();
            this.transferAsset.TransferDate = new Date();
        },
        /*
         * Ẩn form chọn tài sản
         * Author: BATUAN (07/06/2023)
         */
        hideChoosenForm() {
            this.isShowChoosenForm = false;
        },
        /*
         * Hiện form chọn tài sản
         * Author: BATUAN (07/06/2023)
         */
        showChoosenForm() {
            this.isShowChoosenForm = true;
            this.excludedIds = [];
            if (this.transferAsset.TransferAssetDetailList) {
                this.transferAsset.TransferAssetDetailList.forEach((element) => {
                    this.excludedIds.push(element.PropertyId);
                });
            }
        },
        /*
         * Cập nhật lại danh sách các tài sản điều chuyển ở form
         * Author: BATUAN (07/06/2023)
         */
        updateListSelectedProperty(newPropertyTransferList) {
            if (this.formMode == this.$_MISAEnum.formUpdate) {
                newPropertyTransferList.forEach((property) => {
                    for (let i = 0; i < this.transferAssetProps.TransferAssetDetailList.length; i++) {
                        if (this.transferAssetProps.TransferAssetDetailList[i].PropertyId == property.PropertyId) {
                            property.TransferAssetId = this.transferAssetProps.TransferAssetId;
                            property.TransferAssetDetailId =
                                this.transferAssetProps.TransferAssetDetailList[i].TransferAssetDetailId;
                        }
                    }
                });
            }

            if (this.transferAsset.TransferAssetDetailList) {
                this.transferAsset.TransferAssetDetailList =
                    this.transferAsset.TransferAssetDetailList.concat(newPropertyTransferList);
            } else {
                this.transferAsset.TransferAssetDetailList = [...newPropertyTransferList];
            }

            this.paging();
            this.calculateTotalResidualPrice();
            this.calculateTotalOriginalPrice();
            this.checkFullChecked();
            this.totalRecords = this.transferAsset.TransferAssetDetailList.length;
        },
        /*
         * Sự kiện khi click vào icon thêm người nhận
         * Author: BATUAN (07/06/2023)
         */
        handleMoreDeliveryOptions() {
            if (this.$refs['checkbox-delivery'].isChecked) {
                this.$refs['checkbox-delivery'].isChecked = false;
                this.isShowMoreDeliveryOptions = false;
                if (this.formMode == this.$_MISAEnum.formAdd) {
                    this.transferAsset.ReceiverList = [];
                } else if (this.formMode == this.$_MISAEnum.formUpdate) {
                    this.transferAsset.ReceiverList = JSON.parse(JSON.stringify(this.listInitialReceiver));
                }
            } else {
                if (this.formMode == this.$_MISAEnum.formAdd) {
                    this.$refs['checkbox-delivery'].isChecked = true;
                    this.isShowMoreDeliveryOptions = true;
                    this.transferAsset.ReceiverList = [];
                    this.transferAsset.ReceiverList.push({
                        ReceiverOrder: 1,
                    });
                } else if (this.formMode == this.$_MISAEnum.formUpdate) {
                    this.$refs['checkbox-delivery'].isChecked = true;
                    this.isShowMoreDeliveryOptions = true;
                    if (!this.transferAsset.ReceiverList || this.transferAsset.ReceiverList.length <= 0) {
                        this.transferAsset.ReceiverList.push({
                            ReceiverOrder: 1,
                        });
                    }
                }
            }
        },
        /*
         *Sự kiện khi click vào icon thêm trường người nhận
         * Author: BATUAN (07/06/2023)
         */
        addDeliveryInput(index) {
            for (let i = 0; i < this.transferAsset.ReceiverList.length; i++) {
                if (this.transferAsset.ReceiverList[i].ReceiverOrder > index + 1) {
                    this.transferAsset.ReceiverList[i].ReceiverOrder++;
                }
            }
            this.transferAsset.ReceiverList.push({
                ReceiverOrder: index + 2,
            });
            this.transferAsset.ReceiverList.sort((a, b) => a.ReceiverOrder - b.ReceiverOrder);
        },
        /*
         * Sự kiện khi click vào icon xóa trường người nhận
         * Author: BATUAN (07/06/2023)
         */
        deleteDeliveryInput(index) {
            this.transferAsset.ReceiverList = this.transferAsset.ReceiverList.filter((element) => {
                return element.ReceiverOrder != index + 1;
            });
            for (let i = 0; i < this.transferAsset.ReceiverList.length; i++) {
                if (this.transferAsset.ReceiverList[i].ReceiverOrder > index + 1) {
                    this.transferAsset.ReceiverList[i].ReceiverOrder--;
                }
            }
            console.log(this.transferAsset.ReceiverList);
        },
        /*
         * Sự kiện khi click vào icon dịch chuyển dòng người nhận
         * Author: BATUAN (07/06/2023)
         */
        pushUpDeliveryInput(index) {
            if (index != 0) {
                let tmp;
                tmp = this.transferAsset.ReceiverList[index - 1].ReceiverOrder;
                this.transferAsset.ReceiverList[index - 1].ReceiverOrder =
                    this.transferAsset.ReceiverList[index].ReceiverOrder;
                this.transferAsset.ReceiverList[index].ReceiverOrder = tmp;
            }
            this.transferAsset.ReceiverList.sort((a, b) => a.ReceiverOrder - b.ReceiverOrder);
        },
        /*
         * Sự kiện khi click vào icon dịch chuyển dòng người nhận
         * Author: BATUAN (07/06/2023)
         */
        pushDownDeliveryInput(index) {
            if (index < this.transferAsset.ReceiverList.length - 1) {
                let tmp;
                tmp = this.transferAsset.ReceiverList[index + 1].ReceiverOrder;
                this.transferAsset.ReceiverList[index + 1].ReceiverOrder =
                    this.transferAsset.ReceiverList[index].ReceiverOrder;
                this.transferAsset.ReceiverList[index].ReceiverOrder = tmp;
            }
            this.transferAsset.ReceiverList.sort((a, b) => a.ReceiverOrder - b.ReceiverOrder);
        },
        /*
         * Phân trang dữ liệu phía client
         * Author: BATUAN (07/06/2023)
         */
        async paging() {
            this.isLoadingData = true;
            let beginIndex = (this.currentPage - 1) * this.pageSize;
            let endIndex = beginIndex + Number(this.pageSize);
            if (this.transferAsset.TransferAssetDetailList) {
                let transferAssetCopyList = [...this.transferAsset.TransferAssetDetailList];
                if (this.inputFilter) {
                    transferAssetCopyList = transferAssetCopyList.filter((element) => {
                        return (
                            element.PropertyCode.toLowerCase().includes(this.inputFilter.trim().toLowerCase()) ||
                            element.PropertyName.toLowerCase().includes(this.inputFilter.trim().toLowerCase())
                        );
                    });
                }
                this.propertyTransferListPaging = transferAssetCopyList.slice(beginIndex, endIndex);
                this.numberPages = Math.ceil(transferAssetCopyList.length / this.pageSize);
                this.totalRecords = transferAssetCopyList.length;
            }
            await delay(400);
            this.isLoadingData = false;
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
        /*
         * Sự kiện khi click vào ô checkbox all
         * Author: BATUAN (07/06/2023)
         */
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
        /*
         * Sự kiện khi click vào ô checkbox
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
        /*
         * Sự kiện khi click vào 1 hàng trong table
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
                if (
                    !this.selectedRow.some((row) => {
                        return row.PropertyId == id;
                    })
                ) {
                    this.selectedRow.push(this.propertyTransferListPaging[index]);
                } else {
                    this.selectedRow = this.selectedRow.filter((row) => {
                        return row.PropertyId != id;
                    });
                }
            } else if (event.shiftKey) {
                if (this.firstClickRow || this.firstClickRow === 0) {
                    this.selectedRow = this.selectedRow.filter((prop) => {
                        return !this.propertyTransferListPaging.some((item) => item.PropertyId == prop.PropertyId);
                    });
                    this.secondClickRow = index;
                    if (this.secondClickRow > this.firstClickRow) {
                        for (let i = this.firstClickRow; i <= this.secondClickRow; i++) {
                            const property = this.propertyTransferListPaging[i];
                            if (!this.selectedRow.some((item) => item.PropertyId == property.PropertyId)) {
                                this.selectedRow.push(property);
                            }
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
                this.handleClickOnRow(index);
            }
            this.checkForCheckbox();
            this.checkFullChecked();
        },
        /*
         * Gọi Api thêm mới chứng từ điều chuyển
         * Author: BATUAN (29/06/2023)
         */
        async createNewTransferAsset() {
            if (this.validateData() && this.validateDataMajor()) {
                this.transferAsset.OriginalPrice = 0;
                for (let i = 0; i < this.transferAsset.TransferAssetDetailList.length; i++) {
                    this.transferAsset.OriginalPrice += this.transferAsset.TransferAssetDetailList[i].OriginalPrice;
                }

                this.$emit('createdNewTransferAsset', this.transferAsset);
            }
        },
        async btnSaveAction() {
            // chuyển đổi lại id phòng ban chỉnh sửa
            if (this.transferAsset && this.transferAsset.TransferAssetDetailList) {
                this.transferAsset.TransferAssetDetailList.forEach((transferDetail) => {
                    transferDetail.DepartmentTransferId = this.departmentNames.find((element) => {
                        return element.label == transferDetail.DepartmentTransferName;
                    }).value;
                });
            }

            switch (this.formMode) {
                case this.$_MISAEnum.formAdd:
                    await this.createNewTransferAsset();
                    break;
                case this.$_MISAEnum.formUpdate:
                    if (JSON.stringify(this.transferAsset) != JSON.stringify(this.transferAssetProps)) {
                        this.handleUpdateTransferAsset();
                    } else {
                        this.showDialog(this.$_MISAResource['vn-VI'].propertyTransferForm.noChangeToSave);
                    }
                    break;
                default:
                    break;
            }
        },
        /*
         * Gọi Api kiểm tra tài sản được phép xóa ở form thêm hay không
         * Author: BATUAN (29/08/2023)
         */
        async checkDeleteRow(row) {
            if (
                this.listInitialTransferAssetDetail &&
                this.listInitialTransferAssetDetail.some((transferAssetDetail) => {
                    return transferAssetDetail.PropertyId == row.PropertyId;
                })
            ) {
                let propertyIds = [];
                propertyIds.push(row.PropertyId);
                await request
                    .insertRecord(
                        `TransferAsset/CheckTransferAsset?transferAssetId=${this.transferAsset.TransferAssetId}`,
                        propertyIds,
                    )
                    .then(() => {
                        this.transferAsset.TransferAssetDetailList = this.transferAsset.TransferAssetDetailList.filter(
                            (element) => {
                                return element.PropertyId != row.PropertyId;
                            },
                        );
                        this.listTemporaryDelete = [];
                        this.listTemporaryDelete.push(row);
                        this.paging();
                        this.calculateTotalResidualPrice();
                        this.calculateTotalOriginalPrice();
                    })
                    .catch((err) => {
                        this.handleException(err.statusCode, err.message, err.documentInfo, this.showDialog);
                    });
            } else {
                this.transferAsset.TransferAssetDetailList = this.transferAsset.TransferAssetDetailList.filter(
                    (element) => {
                        return element.PropertyId != row.PropertyId;
                    },
                );
                this.paging();
                this.calculateTotalResidualPrice();
                this.calculateTotalOriginalPrice();
            }
        },
        /*
         * Gọi Api kiểm tra tài sản được phép xóa ở form thêm hay không khi thực hiện xóa nhiều
         * Author: BATUAN (29/08/2023)
         */
        async checkDeleteRows() {
            let propertyIds = [];
            if (this.listInitialTransferAssetDetail) {
                this.selectedRow.forEach((row) => {
                    if (
                        this.listInitialTransferAssetDetail.some((element) => {
                            return element.PropertyId == row.PropertyId;
                        })
                    ) {
                        propertyIds.push(row.PropertyId);
                    }
                });

                await request
                    .insertRecord(
                        `TransferAsset/CheckTransferAsset?transferAssetId=${this.transferAsset.TransferAssetId}`,
                        propertyIds,
                    )
                    .then(() => {
                        this.transferAsset.TransferAssetDetailList = this.transferAsset.TransferAssetDetailList.filter(
                            (element) => {
                                return !JSON.Stringify(this.selectedRow).includes(JSON.Stringify(element));
                            },
                        );
                        this.paging();
                        this.calculateTotalResidualPrice();
                        this.calculateTotalOriginalPrice();
                    })
                    .catch((err) => {
                        this.handleException(err.statusCode, err.message, err.documentInfo, this.showDialog);
                    });
            } else {
                this.transferAsset.TransferAssetDetailList = this.transferAsset.TransferAssetDetailList.filter(
                    (element) => {
                        return !JSON.Stringify(this.selectedRow).includes(JSON.Stringify(element));
                    },
                );
                this.paging();
                this.calculateTotalResidualPrice();
                this.calculateTotalOriginalPrice();
            }
        },
        /*
         * Kiểm tra và phân loại các tài sản điều chuyển trong form sửa chứng từ
         * Author: BATUAN (29/08/2023)
         */
        checkInforTransferAssetDetail() {
            this.transferAssetDetailDelete = [];
            this.transferAssetDetailUpdate = [];
            this.transferAssetDetailNochange = [];
            this.transferAssetDetailAdd = [];
            let listTransferAssetCheck = [];
            this.transferAsset.TransferAssetDetailList.forEach((transferAssetDetail) => {
                if (
                    this.listInitialTransferAssetDetail.some((assetDetail) => {
                        return assetDetail.PropertyCode == transferAssetDetail.PropertyCode;
                    })
                ) {
                    // kiểm tra xem 2 cái bằng nhau hay không
                    let check = false;
                    for (let i = 0; i < this.listInitialTransferAssetDetail.length; i++) {
                        if (
                            this.listInitialTransferAssetDetail[i].DepartmentTransferId ==
                                transferAssetDetail.DepartmentTransferId &&
                            this.listInitialTransferAssetDetail[i].Reason == transferAssetDetail.Reason
                        ) {
                            check = true;
                            break;
                        }
                    }
                    if (check == false) {
                        transferAssetDetail.StatusRecord = this.$_MISAEnum.statusRecord.update;
                        this.transferAssetDetailUpdate.push(transferAssetDetail);
                        listTransferAssetCheck.push(transferAssetDetail);
                    } else {
                        transferAssetDetail.StatusRecord = this.$_MISAEnum.statusRecord.noChange;
                        this.transferAssetDetailNochange.push(transferAssetDetail);
                        listTransferAssetCheck.push(transferAssetDetail);
                    }
                } else {
                    transferAssetDetail.StatusRecord = this.$_MISAEnum.statusRecord.insert;
                    this.transferAssetDetailAdd.push(transferAssetDetail);
                }
            });

            if (listTransferAssetCheck.length == 0) {
                this.listInitialTransferAssetDetail.forEach((element) => {
                    element.StatusRecord = this.$_MISAEnum.statusRecord.delete;
                    this.transferAssetDetailDelete.push(element);
                });
            } else if (listTransferAssetCheck.length != this.listInitialTransferAssetDetail.length) {
                this.listInitialTransferAssetDetail.forEach((transferAssetDetail) => {
                    if (
                        !listTransferAssetCheck.some((element) => {
                            return element.PropertyCode == transferAssetDetail.PropertyCode;
                        })
                    ) {
                        transferAssetDetail.StatusRecord = this.$_MISAEnum.statusRecord.delete;
                        this.transferAssetDetailDelete.push(transferAssetDetail);
                    }
                });
            }

            this.transferAsset.TransferAssetDetailList = [];
            this.transferAssetDetailDelete.forEach((element) =>
                this.transferAsset.TransferAssetDetailList.push(element),
            );
            this.transferAssetDetailUpdate.forEach((element) =>
                this.transferAsset.TransferAssetDetailList.push(element),
            );
            this.transferAssetDetailNochange.forEach((element) =>
                this.transferAsset.TransferAssetDetailList.push(element),
            );
            this.transferAssetDetailAdd.forEach((element) => {
                element.TransferAssetId = this.transferAsset.TransferAssetId;
                this.transferAsset.TransferAssetDetailList.push(element);
            });
        },
        /*
         * Kiểm tra và phân loại danh sách người nhận
         * Author: BATUAN (29/08/2023)
         */
        checkInforReceiver() {
            this.transferAsset.receiverList = [];
            this.receiverDelete = [];
            this.receiverUpdate = [];
            this.receiverNochange = [];

            this.transferAsset.ReceiverList.forEach((receiver) => {
                if (receiver.ReceiverId == undefined || receiver.ReceiverId == '00000000-0000-0000-0000-000000000000') {
                    receiver.StatusRecord = this.$_MISAEnum.statusRecord.insert;
                    this.receiverAdd.push(receiver);
                } else if (JSON.stringify(this.listInitialReceiver).includes(JSON.stringify(receiver))) {
                    receiver.StatusRecord = this.$_MISAEnum.statusRecord.noChange;
                    this.receiverNochange.push(receiver);
                } else {
                    receiver.StatusRecord = this.$_MISAEnum.statusRecord.update;
                    this.receiverUpdate.push(receiver);
                }
            });

            this.listInitialReceiver.forEach((receiver) => {
                if (
                    !this.transferAsset.ReceiverList.some((element) => {
                        return element.ReceiverId == receiver.ReceiverId;
                    })
                ) {
                    receiver.StatusRecord = this.$_MISAEnum.statusRecord.delete;
                    this.receiverDelete.push(receiver);
                }
            });

            this.receiverDelete.forEach((element) => this.transferAsset.receiverList.push(element));
            this.receiverUpdate.forEach((element) => this.transferAsset.receiverList.push(element));
            this.receiverNochange.forEach((element) => this.transferAsset.receiverList.push(element));
            this.receiverAdd.forEach((element) => {
                element.TransferAssetId = this.transferAsset.TransferAssetId;
                this.transferAsset.receiverList.push(element);
            });
        },
        /*
         * Hàm gọi emit update chứng từ
         * Author: BATUAN (29/08/2023)
         */
        handleUpdateTransferAsset() {
            if (this.validateData() && this.validateDataMajor()) {
                let listTransferAsset = [];
                listTransferAsset.push(this.transferAsset);
                this.checkInforTransferAssetDetail();
                this.checkInforReceiver();
                this.$emit('updateTransferAsset', listTransferAsset);
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
            let refErrorName = '';
            if (
                this.transferAsset.TransferAssetCode == undefined ||
                this.transferAsset.TransferAssetCode.trim().length == 0
            ) {
                textDialog += 'Mã chứng từ không được phép trống !</br>';
                refErrorName = 'transferAssetCodeInput';
            }
            if (!this.transferAsset.TransferDate) {
                textDialog += 'Ngày điều chuyển không được phép trống !<br>';
                refErrorName = refErrorName ? refErrorName : 'transferDateInput';
            }
            if (!this.transferAsset.TransactionDate) {
                textDialog += 'Ngày chứng từ không được phép trống !<br>';
                refErrorName = refErrorName ? refErrorName : 'transactionDateInput';
            }
            if (this.$refs['checkbox-delivery'].isChecked) {
                for (let i = 0; i < this.transferAsset.ReceiverList.length; i++) {
                    if (
                        this.transferAsset.ReceiverList[i].FullName == undefined ||
                        this.transferAsset.ReceiverList[i].FullName.trim() == '' ||
                        this.transferAsset.ReceiverList[i].Represent == undefined ||
                        this.transferAsset.ReceiverList[i].Represent.trim() == '' ||
                        this.transferAsset.ReceiverList[i].Position == undefined ||
                        this.transferAsset.ReceiverList[i].Position.trim() == ''
                    ) {
                        textDialog += 'Thông tin ban giao nhận không được để trống !<br>';
                        break;
                    }
                }
            }
            if (!this.transferAsset.TransferAssetDetailList) {
                textDialog += 'Danh sách tài sản điều chuyển không được để trống !<br>';
            } else if (
                this.transferAsset.TransferAssetDetailList &&
                this.transferAsset.TransferAssetDetailList.length == 0
            ) {
                textDialog += 'Danh sách tài sản điều chuyển không được để trống !<br>';
            }

            if (textDialog) {
                check = false;
                this.showDialog(textDialog, '', {
                    thirdDialogBtnText: this.$_MISAResource['vn-VI'].close,
                    thirdBtnFunction: () => {
                        this.hideDialog();
                        this.focusOnErrorInput(refErrorName);
                    },
                });
            }

            return check;
        },
        /*
         * validate nghiệp vụ của chứng từ trước khi gửi lên server
         * Author: BATUAN (29/06/2023)
         */
        validateDataMajor() {
            let textDialog = '';
            let check = true;
            let refErrorName = '';
            if (this.transferAsset.TransferDate < this.transferAsset.TransactionDate) {
                textDialog = 'Ngày chứng từ phải nhỏ hơn ngày điều chuyển !<br>';
                refErrorName = refErrorName ? refErrorName : 'transferDateInput';
                check = false;
            } else if (!this.checkDepartmentTransfer().check) {
                textDialog = this.checkDepartmentTransfer().errorMessage;
                refErrorName = '';
                check = false;
            }

            if (textDialog) {
                check = false;
                this.showDialog(textDialog, '', {
                    thirdDialogBtnText: this.$_MISAResource['vn-VI'].close,
                    thirdBtnFunction: () => {
                        this.hideDialog();
                        this.focusOnErrorInput(refErrorName);
                    },
                });
            }
            return check;
        },
        /*
         * Kiểm tra các phòng ban chuyển đi có khác phòng ban hiện tại không
         * Author: BATUAN (29/06/2023)
         */
        checkDepartmentTransfer() {
            let errorMessage = '';
            let check = true;
            for (let i = 0; i < this.transferAsset.TransferAssetDetailList.length; i++) {
                if (
                    this.transferAsset.TransferAssetDetailList[i].DepartmentId ==
                    this.transferAsset.TransferAssetDetailList[i].DepartmentTransferId
                ) {
                    errorMessage = `Bộ phận chuyển đi phải khác bộ phận cũ (<strong>${this.transferAsset.TransferAssetDetailList[i].PropertyCode}</strong>) !`;
                    check = false;
                    break;
                }
            }
            return {
                errorMessage,
                check,
            };
        },
        /*
         * focus vào ô input lỗi nếu có
         * Author: BATUAN (29/06/2023)
         */
        focusOnErrorInput(errorField) {
            if (errorField) {
                this.$refs[errorField].autoFocus();
            }
        },
        /*
         * Hiển thị dialog
         * Author: BATUAN (29/08/2023)
         */
        showDialog(
            textDialog,
            documentInfo,
            dialogActions = {
                thirdDialogBtnText: this.$_MISAResource['vn-VI'].close,
                thirdBtnFunction: this.hideDialog,
            },
        ) {
            this.isShowDialog = true;
            this.textDialog = textDialog;
            this.documentInfo = documentInfo;
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
        handleException(statusCode, message, documentInfo, showDialog) {
            exception(statusCode, message, documentInfo, showDialog);
        },
        /*
         * Format giá trị tiền
         * Author: BATUAN (27/05/2023)
         */
        formatedMoney(value) {
            return formatMoney(value);
        },
        /*
         * Tính toán tổng giá trị còn lại của các tài sản
         * Author: BATUAN (30/08/2023)
         */
        calculateTotalResidualPrice() {
            this.totalResidualPrice = 0;
            this.transferAsset.TransferAssetDetailList.forEach((element) => {
                this.totalResidualPrice += element.ResidualPrice;
            });
        },
        /*
         * Tính toán tổng nguyên giá của các tài sản
         * Author: BATUAN (30/08/2023)
         */
        calculateTotalOriginalPrice() {
            this.totalOriginalPrice = 0;
            this.transferAsset.TransferAssetDetailList.forEach((element) => {
                this.totalOriginalPrice += element.OriginalPrice;
            });
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
         * Sự kiện click vào ô thêm người nhận gần nhất
         * Author: BATUAN (14/06/2023)
         */
        async handleClickAddLastestReceiver() {
            if (this.$refs.checkboxAddReceiver.isChecked == true) {
                this.$refs.checkboxAddReceiver.isChecked = false;
                if (this.formMode == this.$_MISAEnum.formAdd) {
                    this.transferAsset.ReceiverList = [];
                    this.transferAsset.ReceiverList.push({
                        ReceiverOrder: 1,
                    });
                } else if (this.formMode == this.$_MISAEnum.formUpdate) {
                    if (this.listInitialReceiver.length > 0) {
                        this.transferAsset.ReceiverList = JSON.parse(JSON.stringify(this.listInitialReceiver));
                    } else {
                        this.transferAsset.ReceiverList = []
                        this.transferAsset.ReceiverList.push({
                            ReceiverOrder: 1,
                        });
                    }
                }
            } else {
                this.$refs.checkboxAddReceiver.isChecked = true;
                await this.getLastestReceiver();

                if (this.listReceiverLastest.length > 0) {
                    this.transferAsset.ReceiverList = [];
                    this.transferAsset.ReceiverList = [...this.listReceiverLastest];
                }
            }
        },
        /*
         * Sự kiện khi scroll table để đóng combo-box
         * Author: BATUAN (24/08/2023)
         */
        handleScrollBodyTable() {
            for (let i = 0; i < this.$refs.combobox.length; i++) {
                if (this.$refs.combobox[i].$refs.myComboBox.visible == true) {
                    this.$refs.combobox[i].autoBlur();
                }
            }
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
        /*
         * Sự kiện Click vào nút X hoặc Hủy hoặc Esc để đóng from
         * Author: BATUAN (24/08/2023)
         */
        handleClickClose() {
            switch (this.formMode) {
                case this.$_MISAEnum.formUpdate:
                    if (JSON.stringify(this.transferAsset) == JSON.stringify(this.transferAssetProps)) {
                        this.$emit('hideTransferForm');
                    } else {
                        this.showDialog(
                            this.$_MISAResource['vn-VI'].propertyTransferForm.cancelUpdateTransferAsset,
                            [],
                            {
                                firstDialogBtnText: this.$_MISAResource['vn-VI'].cancel,
                                firstBtnFunction: () => {
                                    this.hideDialog();
                                },
                                thirdDialogBtnText: this.$_MISAResource['vn-VI'].yes,
                                thirdBtnFunction: () => {
                                    this.$emit('hideTransferForm');
                                },
                            },
                        );
                    }
                    break;
                case this.$_MISAEnum.formAdd:
                    this.showDialog(this.$_MISAResource['vn-VI'].propertyTransferForm.cancelAddTransferAsset, [], {
                        firstDialogBtnText: this.$_MISAResource['vn-VI'].cancel,
                        firstBtnFunction: () => {
                            this.hideDialog();
                        },
                        thirdDialogBtnText: this.$_MISAResource['vn-VI'].yes,
                        thirdBtnFunction: () => {
                            this.$emit('hideTransferForm');
                        },
                    });
                    break;
                default:
                    break;
            }
        },
    },
};
</script>

<style scoped>
@import url(../../css/views/property/property-transfer-form.css);
@import url(../../css/components/icon.css);
@import url(../../css/components/button.css);
</style>
