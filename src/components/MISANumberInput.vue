<template>
    <div class="number-field__input">
        <input
            ref="myInputNumber"
            type="number"
            class="number-field__input--value"
            :class="[{ 'number-field__input--error': error }, individualClass, , { 'text__input--icon': isShowIcon }]"
            :value="modelValue"
            @input="$emit('update:modelValue', Number($event.target.value))"
            v-on:blur="onBlurFunction"
            v-on:click="onChangeFunction"
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
    name: 'MISANumberInput',
    props: {
        label: String,
        individualClass: String,
        modelValue: Number,
        isRequired: Boolean,
        errorMsg: String,
        isShowIcon: Boolean,
        type: String,
    },
    data() {
        return {
            error: false,
        };
    },
    emit: ['update:modelValue'],
    methods: {
        increasingValue() {
            this.$emit('update:modelValue', Number(Number(this.modelValue) + 1));
        },
        decreasingValue() {
            if (this.modelValue > 0) {
                this.$emit('update:modelValue', Number(Number(this.modelValue) - 1));
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

        unformatNumber(value) {
            const number = parseFloat(value.replace(/[^0-9.-]+/g, ''));
            if (isNaN(number)) {
                return value;
            }
            return number;
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
