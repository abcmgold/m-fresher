<template>
    <m-modal>
        <div class="popup">
            <div class="popup__header">
                <div class="popup__header__title">{{ this.title }}</div>
                <div id="btnCloseModalAdd" class="popup__header__close" @click="$emit('hideAddProperty')">
                    <div class="icon--close" title="Đóng"></div>
                </div>
            </div>
            <div class="popup__container">
                <div class="popup__row">
                    <div class="popup__row-col-3">
                        <div class="popup__item popup__item-force">
                            <!-- <div class="input-group">
                                <div class="input__text--label">
                                    Mã tài sản
                                    <div class="input__icon--force">*</div>
                                </div>
                                <m-text-input
                                    ref="propertyCodeInput"
                                    v-model="property.PropertyCode"
                                    placeholder="Nhập mã tài sản"
                                    :isRequired="true"
                                    type="text-field"
                                ></m-text-input>
                            </div> -->
                            <m-group-input
                                text="Mã tài sản"
                                :isForce="true"
                                message="Mã tài sản không được phép để trống"
                            >
                                <m-text-input
                                    ref="propertyCodeInput"
                                    v-model="property.PropertyCode"
                                    placeholder="Nhập mã tài sản"
                                    :isRequired="true"
                                    type="text-field"
                                ></m-text-input>
                            </m-group-input>
                        </div>
                    </div>
                    <div class="popup__row-col-6" label="propertyName">
                        <div class="popup__item popup__item-force">
                            <!-- <div class="input-group">
                                <div class="input__text--label">
                                    Tên tài sản
                                    <div class="input__icon--force">*</div>
                                </div>
                                <m-text-input
                                    ref="propertyNameInput"
                                    v-model="property.PropertyName"
                                    placeholder="Nhập tên tài sản"
                                    errorMsg="Tên tài sản"
                                    :isRequired="true"
                                    type="text-field"
                                ></m-text-input>
                            </div> -->
                            <m-group-input
                                text="Tên tài sản"
                                :isForce="true"
                                message="Tên tài sản không được phép để trống"
                            >
                                <m-text-input
                                    ref="propertyNameInput"
                                    v-model="property.PropertyName"
                                    placeholder="Nhập tên tài sản"
                                    errorMsg="Tên tài sản"
                                    :isRequired="true"
                                    type="text-field"
                                ></m-text-input>
                            </m-group-input>
                        </div>
                    </div>
                </div>
                <div class="popup__row">
                    <div class="popup__row-col-3">
                        <div class="popup__item popup__item-force">
                            <!-- <div class="input-group">
                                <div class="popup__item-label text__label">
                                    Mã bộ phận sử dụng
                                    <div class="popup__item-icon-force">*</div>
                                </div>
                                <m-combo-box
                                    ref="departmentUseInput"
                                    v-model="property.DepartmentCode"
                                    :isRequired="true"
                                    :dataSelect="this.DepartmentCodes"
                                    placeholder="Nhập mã bộ phận sử dụng"
                                    errorMsg="Mã bộ phận sử dụng"
                                    type="combo-box"
                                ></m-combo-box>
                            </div> -->
                            <m-group-input
                                text="Mã bộ phận sử dụng"
                                :isForce="true"
                                message="Mã bộ phận sử dụng không được phép để trống"
                            >
                                <m-combo-box
                                    ref="departmentUseInput"
                                    v-model="property.DepartmentCode"
                                    :isRequired="true"
                                    :dataSelect="this.DepartmentCodes"
                                    placeholder="Nhập mã bộ phận sử dụng"
                                    errorMsg="Mã bộ phận sử dụng"
                                    type="combo-box"
                                ></m-combo-box>
                            </m-group-input>
                        </div>
                    </div>
                    <div class="popup__row-col-6">
                        <div class="popup__item">
                            <div class="input-group">
                                <div class="input__text--label">Tên bộ phận sử dụng</div>
                                <m-text-input
                                    v-model="property.DepartmentUse"
                                    :isRequired="true"
                                    :isDisabled="true"
                                ></m-text-input>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="popup__row">
                    <div class="popup__row-col-3">
                        <!-- <div class="popup__item popup__item-force">
                            <div class="popup__item-label text__label">
                                Mã loại tài sản
                                <div class="popup__item-icon-force">*</div>
                            </div>

                            <m-combo-box
                                ref="propertyTypeInput"
                                v-model="property.PropertyTypeCode"
                                :isRequired="true"
                                :dataSelect="this.PropertyTypeCodes"
                                errorMsg="Mã loại tài sản"
                                placeholder="Nhập mã loại tài sản"
                                type="combo-box"
                            ></m-combo-box>
                            
                        </div> -->
                        <m-group-input
                            text="Mã loại tài sản"
                            :isForce="true"
                            message="Mã loại tài sản không được phép để trống"
                        >
                            <m-combo-box
                                ref="propertyTypeInput"
                                v-model="property.PropertyTypeCode"
                                :isRequired="true"
                                :dataSelect="this.PropertyTypeCodes"
                                errorMsg="Mã loại tài sản"
                                placeholder="Nhập mã loại tài sản"
                                type="combo-box"
                            ></m-combo-box>
                        </m-group-input>
                    </div>
                    <div class="popup__row-col-6">
                        <div class="popup__item">
                            <div class="input-group">
                                <div class="input__text--label">Tên loại tài sản</div>
                                <m-text-input
                                    v-model="property.PropertyType"
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
                            <!-- <div class="input-group">
                                <div class="input__text--label">
                                    Số lượng
                                    <div class="popup__item-icon-force">*</div>
                                </div>
                                <m-number-input
                                    ref="quantityInput"
                                    v-model="property.Quantity"
                                    :isRequired="true"
                                    errorMsg="Số lượng"
                                    :isShowIcon="true"
                                    type="number-field"
                                ></m-number-input>
                            </div> -->
                            <m-group-input text="Số lượng" :isForce="true" message="Số lượng không được phép để trống">
                                <m-number-input
                                    ref="quantityInput"
                                    v-model="property.Quantity"
                                    :isRequired="true"
                                    errorMsg="Số lượng"
                                    :isShowIcon="true"
                                    type="number-field"
                                ></m-number-input>
                            </m-group-input>
                        </div>
                    </div>
                    <div class="popup__row-col-3">
                        <div class="popup__item">
                            <!-- <div class="input-group">
                                <div class="input__text--label popup__item-force">
                                    Nguyên giá
                                    <div class="popup__item-icon-force">*</div>
                                </div>
                                <m-number-input
                                    ref="originalValueInput"
                                    v-model="property.OriginalValue"
                                    :isRequired="true"
                                    individualClass="input__value--left"
                                    errorMsg="Nguyên giá"
                                    type="number-field"
                                ></m-number-input>
                            </div> -->
                            <m-group-input
                                text="Nguyên giá"
                                :isForce="true"
                                message="Nguyên giá không được phép để trống"
                            >
                                <m-number-input
                                    ref="originalValueInput"
                                    v-model="property.OriginalValue"
                                    :isRequired="true"
                                    individualClass="input__value--left"
                                    errorMsg="Nguyên giá"
                                    type="number-field"
                                ></m-number-input>
                            </m-group-input>
                        </div>
                    </div>
                    <div class="popup__row-col-3">
                        <div class="popup__item popup__item-force">
                            <!-- <div class="input-group">
                                <div class="input__text--label">
                                    Số năm sử dụng
                                    <div class="popup__item-icon-force">*</div>
                                </div>
                                <m-number-input
                                    ref="numberYearsUseInput"
                                    v-model="property.NumberYearsUse"
                                    :isRequired="true"
                                    individualClass="input__value--left"
                                    errorMsg="Số năm sử dụng"
                                    type="number-field"
                                ></m-number-input>
                            </div> -->
                            <m-group-input
                                text="Số năm sử dụng"
                                :isForce="true"
                                message="Số năm sử dụng không được phép để trống"
                            >
                                <m-number-input
                                    ref="numberYearsUseInput"
                                    v-model="property.NumberYearsUse"
                                    :isRequired="true"
                                    individualClass="input__value--left"
                                    errorMsg="Số năm sử dụng"
                                    type="number-field"
                                ></m-number-input>
                            </m-group-input>
                        </div>
                    </div>
                </div>
                <div class="popup__row">
                    <div class="popup__row-col-3">
                        <div class="popup__item popup__item-force">
                            <!-- <div class="input-group">
                                <div class="input__text--label">
                                    Tỷ lệ hao mòn (%)
                                    <div class="popup__item-icon-force">*</div>
                                </div>
                                <m-number-input
                                    ref="wearRateInput"
                                    v-model="property.WearRate"
                                    :isRequired="true"
                                    errorMsg="Tỷ lệ hao mòn"
                                    :isShowIcon="true"
                                    type="number-field"
                                ></m-number-input>
                            </div> -->
                            <m-group-input
                                text="Tỉ lệ hao mòn (%)"
                                :isForce="true"
                                message="Tỉ lệ hao mòn không được phép để trống"
                            >
                                <m-number-input
                                    ref="wearRateInput"
                                    v-model="property.WearRate"
                                    :isRequired="true"
                                    errorMsg="Tỷ lệ hao mòn"
                                    :isShowIcon="true"
                                    type="number-field"
                                ></m-number-input>
                            </m-group-input>
                        </div>
                    </div>
                    <div class="popup__row-col-3">
                        <div class="popup__item popup__item-force">
                            <!-- <div class="input-group">
                                <div class="input__text--label">
                                    Giá trị hao mòn năm
                                    <div class="popup__item-icon-force">*</div>
                                </div>
                                <m-number-input
                                    ref="limitInput"
                                    v-model="property.Limit"
                                    :isRequired="true"
                                    individualClass="input__value--left"
                                    errorMsg="Giá trị hao mòn năm"
                                    type="number-field"
                                ></m-number-input>
                             
                            </div> -->

                            <m-group-input
                                text="Giá trị hao mòn năm"
                                :isForce="true"
                                message="Giá trị hao mòn năm không được phép để trống"
                                ><m-number-input
                                    ref="limitInput"
                                    v-model="property.Limit"
                                    :isRequired="true"
                                    individualClass="input__value--left"
                                    errorMsg="Giá trị hao mòn năm"
                                    type="number-field"
                                ></m-number-input>
                            </m-group-input>
                        </div>
                    </div>
                    <div class="popup__row-col-3">
                        <div class="popup__item">
                            <div class="input-group">
                                <div class="input__text--label">
                                    Năm theo dõi
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
                                Ngày mua
                                <div class="popup__item-icon-force">*</div>
                            </div>
                            <m-date-picker
                                ref="purchaseDateInput"
                                type="date"
                                isRequired="true"
                                v-model="property.PurchaseDate"
                                errorMsg="Ngày mua"
                            ></m-date-picker>
                        </div>
                        <!-- <m-group-input text="Ngày mua" :isForce="true" message="Ngày mua không được phép để trống"
                            ><m-date-picker
                                ref="purchaseDateInput"
                                type="date"
                                isRequired="true"
                                v-model="property.PurchaseDate"
                                errorMsg="Ngày mua"
                            ></m-date-picker
                        ></m-group-input> -->
                    </div>
                    <div class="popup__row-col-3">
                        <div class="popup__item popup__item-force">
                            <div class="popup__item-label text__label">
                                Ngày bắt đầu sử dụng
                                <div class="popup__item-icon-force">*</div>
                            </div>

                            <m-date-picker
                                ref="useDateInput"
                                type="date"
                                isRequired="true"
                                v-model="property.UseDate"
                                errorMsg="Ngày bắt đầu sử dụng"
                            ></m-date-picker>
                        </div>
                        <!-- <m-group-input
                            text="Ngày bắt đầu sử dụng"
                            :isForce="true"
                            message="Ngày bắt đầu sử dụng không được phép để trống"
                        >
                            <m-date-picker
                                ref="useDateInput"
                                type="date"
                                isRequired="true"
                                v-model="property.UseDate"
                                errorMsg="Ngày bắt đầu sử dụng"
                            ></m-date-picker>
                        </m-group-input> -->
                    </div>
                </div>
            </div>
            <div class="popup__action-btn">
                <m-button
                    individualClass="btn--sub"
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
    <!-- <m-modal v-show="isShowDialogValidate">
        <m-dialog
            :text="this.textDialog"
            :endText="this.validateMsg"
            thirdBtnLabel="Đóng"
            :thirdBtnFunction="hideDialogValidate"
        ></m-dialog>
    </m-modal> -->
</template>

<script scoped>
import { formatCurrentDate } from '@/common/common';
import { MISAResource } from '@/common/resource';
export default {
    name: 'PropertyAdd',
    props: {
        closeAddFunction: Function,
        selectedRow: { type: Object, default: null },
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
            DepartmentCodes: [
                { value: 'Phòng Hành chính Tổng hợp', label: 'HCTH' },
                { value: 'Phòng Kế toán', label: 'KT' },
                { value: 'Phòng Nhân sự', label: 'NS' },
            ],
            PropertyTypeCodes: [
                { value: 'Máy vi tính xách tay', label: 'MTXT' },
                { value: 'Máy tính bàn', label: 'MTB' },
            ],

            title: '',
        };
    },
    created() {
        this.property.PropertyCode = `TS${this.$propertyCodeValue}`;
        this.property.Quantity = 1;
        this.property.OriginalValue = 0;
        this.property.NumberYearsUse = 0;
        this.property.WearRate = 0;
        this.property.Limit = 0;
        this.property.FollowYear = new Date().getFullYear().toString();
        this.property.PurchaseDate = this.formatedCurrentDate();
        this.property.UseDate = this.formatedCurrentDate();

        this.title = this.MISAResource['vn-VI'].addProperty;

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
            const option = this.DepartmentCodes.find((element) => {
                return element.label == newValue;
            });
            if (option) {
                this.property.DepartmentUse = option.value;
            }
        },
        // Cập nhật giá trị 'Tên loại tài sản' khi mã loại tài sản
        'property.PropertyTypeCode': function (newValue) {
            const option = this.PropertyTypeCodes.find((element) => {
                return element.label == newValue;
            });
            if (option) {
                this.property.PropertyType = option.value;
            }
        },
        // Tính toán lại giá trị hao mòn khi nguyên giá thay đổi
        'property.OriginalValue': function (newValue) {
            this.property.Limit = (Number(((this.property.WearRate * newValue) / 100).toFixed(2)) * 100) / 100;
        },
        // Tính toán lại tỷ lệ hao mòn và giá trị hao mòn năm khi số năm sử dụng thay đổi
        'property.NumberYearsUse': function (newValue) {
            console.log(newValue)
            if (newValue == 0) {
                this.property.WearRate = 0;
            } else {
                this.property.WearRate = (Number(((1 / parseInt(newValue)) * 100).toFixed(2)) * 100) / 100;
                this.property.Limit =
                    ((Number((this.property.OriginalValue * this.property.WearRate).toFixed(2)) / 100) * 100) / 100;
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
                if (this.validateMajor()) {
                    // Sửa dữ liệu
                    if (this.selectedRow) {
                        //tính toán lại giá trị còn lại
                        this.property.ResidualValue = this.property.OriginalValue - this.property.Limit;
                        // sửa tài sản
                        this.$emit('updateValueRow', this.property);
                        // show toast thông báo thành công
                        this.$emit('showToastSuccess');
                        // đóng modal thêm tài sản
                        this.$emit('hideAddProperty');
                    }
                    // Thêm tài sản
                    else {
                        //tính toán lại giá trị còn lại
                        this.property.ResidualValue = this.property.OriginalValue - this.property.Limit;
                        // thêm tài sản mới
                        this.$emit('addNewProperty', this.property);
                        // show toast thông báo thành công
                        this.$emit('showToastSuccess');
                        // đóng modal thêm tài sản
                        this.$emit('hideAddProperty');
                    }
                } else {
                    this.isShowDialogValidate = true;
                }
            } else {
                this.isShowDialogValidate = true;
                for (const ref in this.$refs) {
                    if (this.$refs[ref].onBlurFunction) {
                        this.$refs[ref].onBlurFunction();
                    }
                }
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
                        this.textDialog = 'Cần phải nhập dữ liệu ';
                        this.validateMsg = `<<${this.$refs[ref].errorMsg}>>`;
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
                Number(this.property.WearRate.toFixed(2)) != Number((1 / this.property.NumberYearsUse) * 100).toFixed(2)
            ) {
                this.textDialog = 'Tỷ lệ hao mòn phải bằng 1/Số năm sử dụng';
                check = false;
            }
            if (Number(this.property.Limit) > Number(this.property.OriginalValue)) {
                this.textDialog = 'Hao mòn năm phải nhỏ hơn hoặc bằng nguyên giá';
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
