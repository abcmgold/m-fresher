<template>
    <div class="number-field__input">
        <input
            ref="myInputNumber"
            type="text"
            class="number-field__input--value"
            :class="[{ 'number-field__input--error': error }, individualClass, , { 'text__input--icon': isShowIcon }]"
            v-model="currentvalue"
            v-on:blur="onBlurFunction"
            v-on:click="onChangeFunction"
            v-on:change="onChangeFunction"
            @input="checkMaxLength"
            @keydown="blockAlphabets"
        />
        <div class="number-field__input--icons" v-if="isShowIcon">
            <div class="number-field__input--icon--up" @click="this.increasingValue"></div>
            <div class="number-field__input--icon--down" @click="this.decreasingValue"></div>
        </div>
    </div>
</template>

<script>
import { formatMoney } from '@/common/common';
export default {
    name: 'MISAMoneyInput',
    props: {
        label: String,
        individualClass: String,
        modelValue: Number,
        isRequired: Boolean,
        errorMsg: String,
        isShowIcon: Boolean,
        type: String,
        maxLength: Number,
    },
    data() {
        return {
            error: false,
            currentvalue: null,
        };
    },
    created() {
        this.currentvalue = this.formattedMoney(this.modelValue);
    },
    watch: {
        currentvalue: function (newValue) {
            newValue = this.unformatNumber(newValue);
            this.$emit('update:modelValue', newValue);
            this.currentvalue = this.formattedMoney(newValue);
            this.error = false;
            if (this.$parent.hideErrorMessage) {
                this.$parent.hideErrorMessage();
            }
        },
        modelValue: function (newValue) {
            this.currentvalue = this.formattedMoney(newValue);
        },
    },
    emit: ['update:modelValue'],
    methods: {
        blockAlphabets(event) {
            // Lấy giá trị của phím được nhấn
            const keyCode = event.keyCode || event.which;
            const key = String.fromCharCode(keyCode);
            if (event.ctrlKey && (keyCode === 65 || keyCode === 86 || keyCode === 67)) {
                // Cho phép sự kiện xảy ra
                return;
            }

            // Kiểm tra nếu ký tự không phải số
            if (!/^[0-9]+$/.test(key) && keyCode !== 8 && keyCode !== 37 && keyCode !== 39 && keyCode !== 9) {
                event.preventDefault(); // Chặn sự kiện nhập
            }
        },
        // Kiểm tra xem ô input vượt quá độ dài max hay chưa
        checkMaxLength() {
            if (this.currentvalue && this.currentvalue.length > this.maxLength) {
                // Giá trị vượt quá maxLength
                this.currentvalue = this.currentvalue.slice(0, this.maxLength);

            }
        },
        increasingValue() {
            this.$emit('update:modelValue', Number(this.modelValue) + 1);
        },
        decreasingValue() {
            if (this.modelValue > 0) {
                this.$emit('update:modelValue', Number(this.modelValue) - 1);
            }
        },
        onBlurFunction() {
            if (this.modelValue === undefined || this.modelValue == '') {
                this.error = true;
                if (this.$parent.showErrorMessage) {
                    this.$parent.showErrorMessage();
                }
            }
        },
        onChangeFunction() {
            this.error = false;
            if (this.$parent.hideErrorMessage) {
                this.$parent.hideErrorMessage();
            }
        },
        formattedMoney(value) {
            return formatMoney(value);
        },

        unformatNumber(money) {
            try {
                if (money) {
                    var value = parseInt(money.replaceAll('.', ''));
                    return value;
                } else {
                    return '';
                }
            } catch (error) {
                console.log(error);
            }
        },
    },
};
</script>

<style scoped>
@import url(@/css/components/numberfield.css);
.number-field__input--value {
    appearance: none;
    -moz-appearance: textfield; /* Firefox */
}

.number-field__input--value::-webkit-inner-spin-button,
.number-field__input--value::-webkit-outer-spin-button {
    -webkit-appearance: none;
    margin: 0;
}
</style>
