<template>
    <m-modal
        v-esc="
            () => {
                this.$emit('hideAddProperty');
            }
        "
    >
        <div class="popup">
            <div class="popup__header">
                <div class="popup__header__title">{{ this.title }}</div>
                <div id="btnCloseModalAdd" class="popup__header__close" @click="this.showDialogCancel">
                    <div
                        :content="this.MISAResource['vn-VI'].close"
                        v-tippy="{ placement: 'bottom' }"
                        class="icon--close"
                    ></div>
                </div>
            </div>
            <div class="popup__container">
                <div class="popup__row">
                    <div class="popup__row-col-3">
                        <div class="popup__item popup__item-force">
                            <m-group-input
                                :text="this.MISAResource['vn-VI'].property.propertyCode"
                                :isForce="true"
                                :message="this.MISAResource['vn-VI'].property.propertyCodeError"
                            >
                                <m-text-input
                                    ref="propertyCodeInput"
                                    v-model="property.PropertyCode"
                                    :placeholder="this.MISAResource['vn-VI'].property.propertyCodeType"
                                    :isRequired="true"
                                    type="text-field"
                                    :maxLength="this.enum.maxLengthCode"
                                    @keydown="this.keydownShiftTab($event)"
                                    :isDisabled="this.isDisableInput"
                                ></m-text-input>
                            </m-group-input>
                        </div>
                    </div>
                    <div class="popup__row-col-6" label="propertyName">
                        <div class="popup__item popup__item-force">
                            <m-group-input
                                :text="this.MISAResource['vn-VI'].property.propertyName"
                                :isForce="true"
                                :message="this.MISAResource['vn-VI'].property.propertyNameError"
                            >
                                <m-text-input
                                    ref="propertyNameInput"
                                    v-model="property.PropertyName"
                                    :placeholder="this.MISAResource['vn-VI'].property.propertyNameType"
                                    :isRequired="true"
                                    type="text-field"
                                    :maxLength="this.enum.maxLengthText"
                                    :isDisabled="this.isDisableInput"
                                ></m-text-input>
                            </m-group-input>
                        </div>
                    </div>
                </div>
                <div class="popup__row">
                    <div class="popup__row-col-3">
                        <div class="popup__item popup__item-force">
                            <m-group-input
                                :text="this.MISAResource['vn-VI'].property.departmentCode"
                                :isForce="true"
                                :message="this.MISAResource['vn-VI'].property.departmentCodeError"
                            >
                                <m-combo-box
                                    ref="departmentUseInput"
                                    v-model="property.DepartmentCode"
                                    :isRequired="true"
                                    :dataSelect="this.departmentCodes"
                                    :placeholder="this.MISAResource['vn-VI'].property.departmentCodeType"
                                    type="combo-box"
                                    url="Department"
                                    :filterable="true"
                                    :isDisabled="this.isDisableInput"
                                ></m-combo-box>
                            </m-group-input>
                        </div>
                    </div>
                    <div class="popup__row-col-6">
                        <div class="popup__item">
                            <div class="input-group">
                                <div class="input__text--label">
                                    {{ this.MISAResource['vn-VI'].property.departmentName }}
                                </div>
                                <m-text-input
                                    v-model="property.DepartmentName"
                                    :isRequired="true"
                                    :isDisabled="true"
                                ></m-text-input>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="popup__row">
                    <div class="popup__row-col-3">
                        <m-group-input
                            :text="this.MISAResource['vn-VI'].property.propertyTypeCode"
                            :isForce="true"
                            :message="this.MISAResource['vn-VI'].property.propertyTypeCodeError"
                        >
                            <m-combo-box
                                ref="propertyTypeInput"
                                v-model="property.PropertyTypeCode"
                                :isRequired="true"
                                :dataSelect="this.propertyTypeCodes"
                                :placeholder="this.MISAResource['vn-VI'].property.propertyTypeCodeType"
                                type="combo-box"
                                url="PropertyType"
                                :filterable="true"
                                :isDisabled="this.isDisableInput"
                            ></m-combo-box>
                        </m-group-input>
                    </div>
                    <div class="popup__row-col-6">
                        <div class="popup__item">
                            <div class="input-group">
                                <div class="input__text--label">
                                    {{ this.MISAResource['vn-VI'].property.propertyTypeName }}
                                </div>
                                <m-text-input
                                    v-model="property.PropertyTypeName"
                                    :isRequired="true"
                                    :isDisabled="true"
                                ></m-text-input>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="popup__row">
                    <div class="popup__row-col-3">
                        <div class="popup__item popup__item-force">
                            <m-group-input
                                :text="this.MISAResource['vn-VI'].property.quantity"
                                :isForce="true"
                                :message="this.MISAResource['vn-VI'].property.quantityError"
                            >
                                <m-money-input
                                    ref="quantityInput"
                                    v-model="property.Quantity"
                                    :isRequired="true"
                                    :isShowIcon="true"
                                    type="number-field"
                                    :maxLength="this.enum.maxLengthNumber"
                                    :isDisabled="this.isDisableInput"
                                ></m-money-input>
                            </m-group-input>
                        </div>
                    </div>
                    <div class="popup__row-col-3">
                        <div class="popup__item">
                            <m-group-input
                                :text="this.MISAResource['vn-VI'].property.originalPrice"
                                :isForce="true"
                                :message="this.MISAResource['vn-VI'].property.originalPriceError"
                            >
                                <m-money-input
                                    ref="originalValueInput"
                                    v-model="property.OriginalPrice"
                                    :isRequired="true"
                                    individualClass="input__value--left"
                                    type="number-field"
                                    :maxLength="this.enum.maxLengthNumber"
                                    :isDisabled="this.isDisableInput"
                                ></m-money-input>
                            </m-group-input>
                        </div>
                    </div>
                    <div class="popup__row-col-3">
                        <div class="popup__item popup__item-force">
                            <m-group-input
                                :text="this.MISAResource['vn-VI'].property.numberYearUse"
                                :isForce="true"
                                :message="this.MISAResource['vn-VI'].property.numberYearUseError"
                            >
                                <m-money-input
                                    ref="numberYearsUseInput"
                                    v-model="property.NumberYearUse"
                                    :isRequired="true"
                                    individualClass="input__value--left"
                                    type="number-field"
                                    :maxLength="this.enum.maxLengthNumber"
                                    :isDisabled="this.isDisableInput"
                                ></m-money-input>
                            </m-group-input>
                        </div>
                    </div>
                </div>
                <div class="popup__row">
                    <div class="popup__row-col-3">
                        <div class="popup__item popup__item-force">
                            <m-group-input
                                :text="this.MISAResource['vn-VI'].property.wearRate"
                                :isForce="true"
                                :message="this.MISAResource['vn-VI'].property.wearRateError"
                            >
                                <m-number-input
                                    ref="wearRateInput"
                                    v-model="property.WearRate"
                                    :isRequired="true"
                                    :isShowIcon="true"
                                    type="number-field"
                                    :maxLength="this.enum.maxLengthNumber"
                                    :isDisabled="this.isDisableInput"
                                ></m-number-input>
                            </m-group-input>
                        </div>
                    </div>
                    <div class="popup__row-col-3">
                        <div class="popup__item popup__item-force">
                            <m-group-input
                                :text="this.MISAResource['vn-VI'].property.wearRateValue"
                                :isForce="true"
                                :message="this.MISAResource['vn-VI'].property.wearRateValueError"
                                ><m-money-input
                                    ref="limitInput"
                                    v-model="property.WearRateValue"
                                    :isRequired="true"
                                    individualClass="input__value--left"
                                    type="number-field"
                                    :maxLength="this.enum.maxLengthNumber"
                                    :isDisabled="this.isDisableInput"
                                ></m-money-input>
                            </m-group-input>
                        </div>
                    </div>
                    <div class="popup__row-col-3">
                        <div class="popup__item">
                            <div class="input-group">
                                <div class="input__text--label">
                                    {{ this.MISAResource['vn-VI'].property.followYear }}
                                    <div class="popup__item-icon-force">*</div>
                                </div>
                                <m-text-input
                                    v-model="property.FollowYear"
                                    :isRequired="true"
                                    :isDisabled="true"
                                    individualClass="input__value--left"
                                ></m-text-input>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="popup__row">
                    <div class="popup__row-col-3">
                        <m-group-input
                            :text="this.MISAResource['vn-VI'].property.purchaseDate"
                            :isForce="true"
                            :message="this.MISAResource['vn-VI'].property.purchaseDateError"
                            ><m-date-picker
                                ref="purchaseDateInput"
                                type="date"
                                :isRequired="true"
                                v-model="property.PurchaseDate"
                                :isDisabled="this.isDisableInput"
                            ></m-date-picker>
                        </m-group-input>
                    </div>
                    <div class="popup__row-col-3">
                        <div class="popup__item popup__item-force">
                            <m-group-input
                                :text="this.MISAResource['vn-VI'].property.useDate"
                                :isForce="true"
                                :message="this.MISAResource['vn-VI'].property.useDateError"
                                ><m-date-picker
                                    ref="useDateInput"
                                    type="date"
                                    :isRequired="true"
                                    v-model="property.FollowDate"
                                    :isDisabled="this.isDisableInput"
                                ></m-date-picker>
                            </m-group-input>
                        </div>
                    </div>
                </div>
            </div>
            <div class="popup__action-btn">
                <m-button
                    individualClass="btn--primary"
                    :label="this.MISAResource['vn-VI'].save"
                    @click="this.addProperty"
                    :isDisabled="this.isDisableInput"
                ></m-button>
                <m-button
                    individualClass="btn--noborder"
                    :label="this.MISAResource['vn-VI'].cancel"
                    @click="this.showDialogCancel"
                    @keydown="this.keydownTab($event)"
                    ref="lastButton"
                ></m-button>
            </div>
        </div>
    </m-modal>
    <m-modal v-if="this.isShowModal">
        <m-dialog
            :type="this.typeDialog"
            :text="this.textDialog"
            :firstBtnFunction="this.firstBtnFunction"
            :secondBtnFunction="this.secondBtnFunction"
            :thirdBtnFunction="this.thirdBtnFunction"
            :firstBtnLabel="this.firstDialogBtnText"
            :secondBtnLabel="this.secondDialogBtnText"
            :thirdBtnLabel="this.thirdDialogBtnText"
            :dialogActions="this.dialogActions"
        ></m-dialog>
    </m-modal>
</template>

<script scoped>
import { formatCurrentDate, formatRatio } from '@/common/common';
import ENUM from '@/common/enum';
import { MISAResource } from '@/common/resource';
import exception from '@/common/exception';
export default {
    name: 'PropertyAdd',
    props: {
        closeAddFunction: Function,
        selectedRow: { type: Object, default: null },
        propertyCodeSample: String,
        departmentCodes: {
            type: Array,
            default: null,
        },
        propertyTypeCodes: {
            type: Array,
            default: null,
        },
        formMode: Number,
    },
    data() {
        return {
            property: {},
            compareProperty: {},
            MISAResource: MISAResource,
            validateMsg: '',
            enum: ENUM,
            title: '',
            isShowModal: '',
            textDialog: '',
            firstDialogBtnText: '',
            secondDialogBtnText: '',
            thirdDialogBtnText: '',
            firstBtnFunction: null,
            secondBtnFunction: null,
            thirdBtnFunction: null,
            dialogActions: {
                firstDialogBtnText: '',
                secondDialogBtnText: '',
                thirdDialogBtnText: '',
                firstBtnFunction: null,
                secondBtnFunction: null,
                thirdBtnFunction: null,
            },
            isDisableInput: 0
        };
    },
    created() {
        switch (this.formMode) {
            case this.enum.formAdd:
                this.title = this.MISAResource['vn-VI'].addProperty;
                // gán các giá trị mặc định
                this.property.PropertyCode = this.propertyCodeSample;
                this.property.Quantity = 1;
                this.property.OriginalPrice = Number(0);
                this.property.NumberYearUse = Number(0);
                this.property.WearRate = Number(0);
                this.property.WearRateValue = Number(0);
                this.property.FollowYear = new Date().getFullYear();
                this.property.PurchaseDate = new Date();
                this.property.FollowDate = new Date();
                break;
            case this.enum.formUpdate:
                this.title = this.MISAResource['vn-VI'].updateProperty;
                this.property = { ...this.selectedRow };
                this.isDisableInput = !this.property.EditMode
                break;
            case this.enum.formDuplicate:
                this.title = this.MISAResource['vn-VI'].addProperty;
                this.property = { ...this.selectedRow };
                this.property.PropertyCode = this.propertyCodeSample;
                break;
            default:
                break;
        }
        this.compareProperty = { ...this.property };
    },
    mounted() {
        // focus vào ô input đầu tiên khi lên form
        const firstKey = Object.keys(this.$refs)[0];
        this.$refs[firstKey].autoFocus();
    },
    unmounted() {
        this.$emit('resetSelectedRow');
    },
    watch: {
        // Cập nhật giá trị 'Tên bộ phận sử dụng' khi mã bộ phận sử dụng thay đổi
        'property.DepartmentCode': function (newValue) {
            const option = this.departmentCodes.find((element) => {
                return element.label == newValue;
            });
            if (option) {
                this.property.DepartmentName = option.value;
                this.property.DepartmentId = option.id;
            }
        },
        // Cập nhật giá trị 'Tên loại tài sản' khi mã loại tài sản
        'property.PropertyTypeCode': function (newValue) {
            const option = this.propertyTypeCodes.find((element) => {
                return element.label == newValue;
            });
            if (option) {
                this.property.PropertyTypeName = option.value;
                this.property.PropertyTypeId = option.id;
                this.property.WearRate = option.wearRate;
                this.property.NumberYearUse = option.numberYearUse;
            }
        },
        // Tính toán lại giá trị hao mòn khi nguyên giá thay đổi
        'property.OriginalPrice': function (newValue) {
            this.property.WearRateValue = (Number(((this.property.WearRate * newValue) / 100).toFixed(2)) * 100) / 100;
        },
        // Tính toán lại tỷ lệ hao mòn và giá trị hao mòn năm khi số năm sử dụng thay đổi
        'property.NumberYearUse': function (newValue) {
            if (newValue == 0) {
                this.property.WearRate = 0;
            } else {
                this.property.WearRate = (Number(((1 / parseInt(newValue)) * 100).toFixed(2)) * 100) / 100;
                this.property.WearRateValue =
                    ((Number((this.property.OriginalPrice * this.property.WearRate).toFixed(2)) / 100) * 100) / 100;
            }
        },
    },

    methods: {
        /*
         * Upload khi ấn vào nút Lưu
         * Author: BATUAN (27/05/2023)
         * ModifiedBy: BATUAN (30/05/2023)
         */
        addProperty() {
            //validate thành công
            if (this.validateData().validate) {
                if (this.validateMajor().check) {
                    switch (this.formMode) {
                        // Thêm mới
                        case this.enum.formAdd:
                            //tính toán lại giá trị còn lại
                            this.property.ResidualValue = this.property.OriginalValue - this.property.WearRateValue;
                            // thêm tài sản mới
                            this.$emit('addNewProperty', this.property);
                            break;
                        // Sửa
                        case this.enum.formUpdate:
                            //tính toán lại giá trị còn lại
                            this.property.ResidualValue = this.property.OriginalValue - this.property.WearRateValue;
                            // sửa tài sản
                            this.$emit('updateValueRow', this.property);
                            break;
                        // Nhân bản
                        case this.enum.formDuplicate:
                            //tính toán lại giá trị còn lại
                            this.property.ResidualValue = this.property.OriginalValue - this.property.WearRateValue;
                            // sửa tài sản
                            this.$emit('addNewProperty', this.property);
                            break;
                        default:
                            break;
                    }
                } else {
                    this.$emit('showDialog', this.textDialog);
                }
            } else {
                // Gọi hàm blur của các ô input
                for (const ref in this.$refs) {
                    if (this.$refs[ref].onBlurFunction) {
                        this.$refs[ref].onBlurFunction();
                    }
                }
                // Focus vào ô input lỗi đầu tiên
                this.focusOnErrorInput();
            }
        },
        /*
         * Show dialog thông báo lỗi validate
         * Author: BATUAN (30/05/2023)
         */
        showDialogValidate(message) {
            this.showDialogValidate = true;
            this.textDialog = message;
        },
        /*
         * Validate dữ liệu của các trường bắt buộc
         * Author: BATUAN (30/05/2023)
         */
        validateData() {
            console.log('validateData');
            let validate = true;
            let refName = '';
            for (const ref in this.$refs) {
                if (this.$refs[ref].isRequired) {
                    if (this.$refs[ref].modelValue === '' || this.$refs[ref].modelValue === undefined) {
                        console.log(ref, this.$refs[ref].modelValue)
                        validate = false;
                        refName = ref;
                        break;
                    }
                }
            }
            return {
                validate,
                refName,
            };
        },
        /*
         * Validate dữ liệu liên quan đến nghiệp vụ
         * Author: BATUAN (30/05/2023)
         */
        validateMajor() {
            console.log('Major')

            let check = true;
            let refName = null;
            if (
                Number(this.property.WearRate.toFixed(2)) != Number((1 / this.property.NumberYearUse) * 100).toFixed(2)
            ) {
                this.textDialog = this.MISAResource['vn-VI'].wearRateError;
                check = false;
                refName = 'numberYearsUseInput';
                console.log(this.property.WearRate.toFixed(2))
            }
            if (Number(this.property.WearRateValue) > Number(this.property.OriginalPrice)) {
                this.textDialog = this.MISAResource['vn-VI'].wearRateValueError;
                check = false;
                refName = 'originalValueInput';
            }
            if (this.property.PurchaseDate > this.property.FollowDate) {
                this.textDialog = this.MISAResource['vn-VI'].dateError;
                check = false;
                refName = 'purchaseDateInput';
            }
            return {
                check,
                refName,
            };
        },
        /*
         * Hiển thị dialog cancel
         * Author: BATUAN (27/05/2023)
         */
        showDialogCancel() {
            if (
                JSON.stringify(this.compareProperty) == JSON.stringify(this.property) &&
                this.formMode != this.enum.formAdd &&
                this.formMode != this.enum.formDuplicate
            ) {
                this.$emit('hideAddProperty');
            } else {
                switch (this.formMode) {
                    case this.enum.formAdd:
                        this.showDialog(this.$_MISAResource['vn-VI'].declareProperty, {
                            firstDialogBtnText: this.$_MISAResource['vn-VI'].no,
                            thirdDialogBtnText: this.$_MISAResource['vn-VI'].yes,
                            firstBtnFunction: this.hideDialog,
                            thirdBtnFunction: this.hideModal,
                        });
                        break;
                    case this.enum.formUpdate:
                        this.showDialog(this.$_MISAResource['vn-VI'].declareProperty, {
                            firstDialogBtnText: this.$_MISAResource['vn-VI'].no,
                            thirdDialogBtnText: this.$_MISAResource['vn-VI'].yes,
                            firstBtnFunction: this.hideDialog,
                            thirdBtnFunction: this.hideModal,
                        });
                        break;
                    case this.enum.formDuplicate:
                        this.showDialog(this.$_MISAResource['vn-VI'].declareProperty, {
                            firstDialogBtnText: this.$_MISAResource['vn-VI'].no,
                            thirdDialogBtnText: this.$_MISAResource['vn-VI'].yes,
                            firstBtnFunction: this.hideDialog,
                            thirdBtnFunction: this.hideModal,
                        });
                        break;
                    default:
                        break;
                }
            }
        },
        showDialog(textDialog, dialogActions) {
            this.isShowModal = true;
            this.textDialog = textDialog;
            this.dialogActions = dialogActions;
        },
        hideDialog() {
            this.isShowModal = false;
        },
        /*
         * Ẩn modals chi tiết tài sản
         * Author: BATUAN (27/05/2023)
         */
        hideModal() {
            this.$emit('closeDialogAndForm');
        },
        /*
         * FormatDate
         * Author: BATUAN (29/05/2023)
         */
        formatedCurrentDate(date) {
            return formatCurrentDate(date);
        },
        /*
         * Format tỷ lệ
         * Author: BATUAN (29/05/2023)
         */
        formatedRatio() {
            return formatRatio();
        },
        /*
         * Focus vào ô input lỗi đầu tiên
         * Author: BATUAN (27/05/2023)
         */
        focusOnErrorInput() {
            if (this.validateData().refName) {
                this.$nextTick(() => {
                    this.$refs[this.validateData().refName].autoFocus();
                });
            } else if (this.validateMajor().refName) {
                this.$nextTick(() => {
                    this.$refs[this.validateMajor().refName].autoFocus();
                });
            }
        },
        /*
         * sự kiện tab ở phần tử cuối cùng thì tự nhảy lên input đầu tiên
         * Author: BATUAN (27/05/2023)
         */
        keydownTab(event) {
            if (event.key === 'Tab') {
                if (!event.shiftKey) {
                    const firstKey = Object.keys(this.$refs)[0];
                    this.$refs[firstKey].autoFocus();
                    event.preventDefault();
                }
            }
        },
        /*
         * sự kiện shift + tab ở phần tử đầu thì tự nhảy xuống button cuối cùng
         * Author: BATUAN (27/05/2023)
         */
        keydownShiftTab(event) {
            if (event.key === 'Tab') {
                if (event.shiftKey) {
                    this.$refs.lastButton.$el.focus();
                    event.preventDefault();
                }
            }
        },
           /*
         * Xử lý mã lỗi backend trả về
         * Author: BATUAN (29/06/2023)
         */
         handleException(statusCode, message, documentInfo, showDialog) {
            exception(statusCode, message, documentInfo, showDialog);
        },
    },
};
</script>

<style scoped>
@import url(../../css/components/popup.css);
@import url(../../css/components/dialog.css);
@import url(../../css/datepicker/datepicker.css);
@import url(../../css/components/popup.css);
@import url(../../css/components/dialog.css);
@import url(@/css/components/inputgroup.css);
</style>
