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
                <div id="btnCloseModalAdd" class="popup__header__close" @click="$emit('hideAddProperty')">
                    <el-tooltip effect="dark" :content="this.MISAResource['vn-VI'].close" placement="bottom-start">
                        <div class="icon--close"></div>
                    </el-tooltip>
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
                                ></m-combo-box>
                            </m-group-input>
                        </div>
                    </div>
                    <div class="popup__row-col-6">
                        <div class="popup__item">
                            <div class="input-group">
                                <div class="input__text--label">{{this.MISAResource['vn-VI'].property.departmentName}}</div>
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
                            ></m-combo-box>
                        </m-group-input>
                    </div>
                    <div class="popup__row-col-6">
                        <div class="popup__item">
                            <div class="input-group">
                                <div class="input__text--label">{{this.MISAResource['vn-VI'].property.propertyTypeName}}</div>
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
                            <m-group-input :text="this.MISAResource['vn-VI'].property.quantity" :isForce="true" :message="this.MISAResource['vn-VI'].property.quantityError">
                                <m-money-input
                                    ref="quantityInput"
                                    v-model="property.Quantity"
                                    :isRequired="true"
                                    :isShowIcon="true"
                                    type="number-field"
                                    :maxLength="this.enum.maxLengthNumber"
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
                                ></m-money-input>
                            </m-group-input>
                        </div>
                    </div>
                    <div class="popup__row-col-3">
                        <div class="popup__item">
                            <div class="input-group">
                                <div class="input__text--label">
                                    {{this.MISAResource['vn-VI'].property.followYear}}
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
                        <div class="popup__item popup__item-force">
                            <div class="popup__item-label text__label">
                                {{this.MISAResource['vn-VI'].property.purchaseDate}}
                                <div class="popup__item-icon-force">*</div>
                            </div>
                            <m-date-picker
                                ref="purchaseDateInput"
                                type="date"
                                :isRequired="true"
                                v-model="property.PurchaseDate"
                            ></m-date-picker>
                        </div>
                    </div>
                    <div class="popup__row-col-3">
                        <div class="popup__item popup__item-force">
                            <div class="popup__item-label text__label">
                                {{this.MISAResource['vn-VI'].property.useDate}}
                                <div class="popup__item-icon-force">*</div>
                            </div>

                            <m-date-picker
                                ref="useDateInput"
                                type="date"
                                :isRequired="true"
                                v-model="property.FollowDate"
                            ></m-date-picker>
                        </div>
                    </div>
                </div>
            </div>
            <div class="popup__action-btn">
                <m-button
                    individualClass="btn--noborder "
                    :label="this.MISAResource['vn-VI'].cancel"
                    @click="this.showDialogCancel"
                ></m-button>
                <m-button
                    individualClass="btn--primary"
                    :label="this.MISAResource['vn-VI'].save"
                    @click="this.addProperty"
                ></m-button>
            </div>
        </div>
    </m-modal>
    <m-modal v-show="isShowDialogCancel">
        <m-dialog
            :text="
                this.selectedRow
                    ? this.MISAResource['vn-VI'].cancelEditting
                    : this.MISAResource['vn-VI'].declareProperty
            "
            :firstBtnLabel="this.MISAResource['vn-VI'].no"
            :thirdBtnLabel="this.MISAResource['vn-VI'].yes"
            :firstBtnFunction="hideDialogCancel"
            :thirdBtnFunction="hideDialogAndModal"
        ></m-dialog>
    </m-modal>
    <m-modal v-show="isShowDialogValidate">
        <m-dialog
            :text="this.textDialog"
            :endText="this.validateMsg"
            thirdBtnLabel="Đóng"
            :thirdBtnFunction="hideDialogValidate"
        ></m-dialog>
    </m-modal>
</template>

<script scoped>
import { formatCurrentDate } from '@/common/common';
import ENUM from '@/common/enum';
import { MISAResource } from '@/common/resource';
import instance from '@/common/instance';
export default {
    name: 'PropertyAdd',
    props: {
        closeAddFunction: Function,
        selectedRow: { type: Object, default: null },
        departmentCodes: {
            type: Array,
            default: null,
        },
        propertyTypeCodes: {
            type: Array,
            default: null,
        },
    },
    data() {
        return {
            property: {},
            compareProperty: {},
            MISAResource: MISAResource,
            isShowDialogCancel: false,
            isShowDialogValidate: false,
            selectedPropertyTypeName: '',
            selectedDepartmentName: '',
            validateMsg: '',
            textDialog: '',
            enum: ENUM,

            title: '',
            propertyCodeSample: '',
        };
    },
    async created() {
        if (!this.selectedRow) {
            // tính giá trị code mới được gợi ý
            await this.generateSampleCode();
            this.title = this.MISAResource['vn-VI'].addProperty;
        }
        // gán các giá trị mặc định
        this.property.PropertyCode = this.propertyCodeSample;
        this.property.Quantity = 1;
        this.property.OriginalPrice = 0;
        this.property.NumberYearUse = 0;
        this.property.WearRate = 0;
        this.property.WearRateValue = 0;
        this.property.FollowYear = new Date().getFullYear().toString();
        this.property.PurchaseDate = this.formatedCurrentDate();
        this.property.FollowDate = this.formatedCurrentDate();

        if (this.selectedRow) {
            this.property = { ...this.selectedRow };
        }
        this.compareProperty = { ...this.property };
    },
    beforeMount() {
        if (this.selectedRow) {
            this.title = this.MISAResource['vn-VI'].updateProperty;
        }
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
         * Tạo mã code mới khi mở form thêm
         * Author: BATUAN (16/06/2023)
         */
        async generateSampleCode() {
            await instance
                .get('Property/GetLastestCode')
                .then((res) => {
                    // Tách phần chữ
                    let textPart = res.data.match(/[A-Za-z]+/)[0];

                    if (!res.data.match(/\d+/)) {
                        let newPropertyCode = textPart + '1';
                        this.propertyCodeSample = newPropertyCode;
                    } else {
                        // Tách phần số từ chuỗi mã
                        let numberPart = res.data.match(/\d+/)[0];
                        // tính độ dài của phần số trong đoạn mã
                        let length = numberPart.length;
                        // chuyển phần số thành 1 số nguyên
                        let number = parseInt(numberPart);
                        // tăng giá trị lên 1
                        number++;
                        // chuyển lại về string
                        number = number.toString();
                        // gắn thêm các số 0 ở trước cho đủ độ dài ban đâu
                        let preZero = '';
                        for (let i = 0; i < length - number.length; i++) {
                            preZero = preZero + '0';
                        }
                        // giá trị sau khi được tăng
                        let newPropertyCode = textPart + preZero + number;
                        this.propertyCodeSample = newPropertyCode;
                    }
                })
                .catch((err) => {
                    console.log(err);
                });
        },
        /*
         * Upload khi ấn vào nút Lưu
         * Author: BATUAN (27/05/2023)
         * ModifiedBy: BATUAN (30/05/2023)
         */
        addProperty() {
            //validate thành công
            if (this.validateData().validate) {
                if (this.validateMajor()) {
                    // Sửa dữ liệu
                    if (this.selectedRow) {
                        //tính toán lại giá trị còn lại
                        this.property.ResidualValue = this.property.OriginalValue - this.property.WearRateValue;
                        // sửa tài sản
                        this.$emit('updateValueRow', this.property);
                      
                    }
                    // Thêm tài sản
                    else {
                        //tính toán lại giá trị còn lại
                        this.property.ResidualValue = this.property.OriginalValue - this.property.WearRateValue;
                        // thêm tài sản mới
                        this.$emit('addNewProperty', this.property);
                    }
                } else {
                    this.isShowDialogValidate = true;
                }
            } else {
                for (const ref in this.$refs) {
                    if (this.$refs[ref].onBlurFunction) {
                        this.$refs[ref].onBlurFunction();
                    }
                }
                this.focusOnErrorInput();
            }
        },
        /*
         * Validate dữ liệu của các trường bắt buộc
         * Author: BATUAN (30/05/2023)
         */
        validateData() {
            let validate = true;
            let refText = '';
            for (const ref in this.$refs) {
                if (this.$refs[ref].isRequired) {
                    if (!this.$refs[ref].modelValue) {
                        validate = false;
                        refText = ref;
                        break;
                    }
                }
            }
            return {
                validate,
                refText,
            };
        },
        /*
         * Validate dữ liệu liên quan đến nghiệp vụ
         * Author: BATUAN (30/05/2023)
         */
        validateMajor() {
            let check = true;
            if (
                Number(this.property.WearRate.toFixed(2)) != Number((1 / this.property.NumberYearUse) * 100).toFixed(2)
            ) {
                this.textDialog = this.MISAResource['vn-VI'].wearRateError;
                check = false;
            }
            if (Number(this.property.WearRateValue) > Number(this.property.OriginalPrice)) {
                this.textDialog = this.MISAResource['vn-VI'].wearRateValueError;
                check = false;
            }
            return check;
        },
        /*
         * Hiển thị dialog cancel
         * Author: BATUAN (27/05/2023)
         */
        showDialogCancel() {
            if (JSON.stringify(this.compareProperty) == JSON.stringify(this.property)) {
                this.$emit('hideAddProperty');
            } else {
                this.isShowDialogCancel = true;
            }
        },
        /*
         * Ẩn modals chi tiết tài sản
         * Author: BATUAN (27/05/2023)
         */
        hideModal() {
            this.$emit('closeDetail');
        },
        /*
         * Ẩn dialog
         * Author: BATUAN (27/05/2023)
         */
        hideDialogCancel() {
            this.isShowDialogCancel = false;
        },
        /*
         * Ẩn dialog thông báo validate
         * Author: BATUAN (27/05/2023)
         */
        hideDialogValidate() {
            this.focusOnErrorInput();
            this.isShowDialogValidate = false;
        },
        /*
         * FormatDate
         * Author: BATUAN (29/05/2023)
         */
        formatedCurrentDate() {
            return formatCurrentDate();
        },
        /*
         * Ẩn dialog cancel và đóng form thêm tài sản
         * Author: BATUAN (27/05/2023)
         */
        hideDialogAndModal() {
            this.$emit('hideAddProperty');
            this.isShowDialogCancel = false;
        },
        /*
         * Focus vào ô input lỗi đầu tiên
         * Author: BATUAN (27/05/2023)
         */
        focusOnErrorInput() {
            if (this.validateData().refText) {
                this.$nextTick(() => {
                    switch (this.$refs[this.validateData().refText].type) {
                        case 'combo-box':
                            this.$refs[this.validateData().refText].$refs.myComboBox.focus();
                            break;
                        case 'text-field':
                            this.$refs[this.validateData().refText].$el.focus();
                            break;
                        case 'number-field':
                            this.$refs[this.validateData().refText].$refs.myInputNumber.focus();
                            break;
                        case 'date':
                            this.$refs[this.validateData().refText].$refs.myDatePicker.openMenu();
                            break;
                        default:
                            this.$refs[this.validateData().refText].$el.focus();
                            break;
                    }
                });
            }
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
