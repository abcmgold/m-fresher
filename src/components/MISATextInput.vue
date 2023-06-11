<template>
    <input
        ref="inputValue"
        type="text-field"
        :class="['text__input', individualClass, { 'text__input-error': error }]"
        :value="modelValue"
        @input="$emit('update:modelValue', $event.target.value)"
        :placeholder="placeholder"
        :disabled="isDisabled"
        v-on:blur="onBlurFunction"
        v-on:change="onChangeFunction"
        @focus="
            () => {
                if (this.$parent.hideErrorMessage) {
                    this.$parent.hideErrorMessage();
                }
            }
        "
    />
</template>

<script>
// import { formatMoney } from '../common/common.js';
export default {
    name: 'MISATextInput',
    props: {
        placeholder: String,
        individualClass: String,
        isDisabled: Boolean,
        modelValue: String,
        onBlur: Function,
        isRequired: Boolean,
        errorMsg: String,
        type: String,
    },
    data() {
        return {
            error: false,
        };
    },

    methods: {
        onBlurFunction() {
            if (this.isRequired) {
                if (this.modelValue === undefined || this.modelValue === '') {
                    this.error = true;
                    if (this.$parent.showErrorMessage) {
                        this.$parent.showErrorMessage();
                    }
                }
            }
        },
        onChangeFunction() {
            this.error = false;
            if (this.$parent.hideErrorMessage) {
                this.$parent.hideErrorMessage();
            }
        },
        // onKeyDown(event) {
        //     if (this.type == 'number') {
        //         const keyCode = event.keyCode || event.which;
        //         const key = String.fromCharCode(keyCode);
        //         const allowedKeys = /[0-9,\b\t\x25\x27]/;

        //         if (!allowedKeys.test(key)) {
        //             event.preventDefault();
        //         }
        //     }
        // },
        formattedMoney(value) {
            const number = parseFloat(value);
            if (isNaN(number)) {
                return value;
            }
            return number.toLocaleString('en-US');
        },

        unformatNumber(value) {
            const number = parseFloat(value.replace(/[^0-9.-]+/g, ''));
            if (isNaN(number)) {
                return value;
            }
            return number.toString();
        },
    },
    emits: ['update:modelValue'],
};
</script>

<style scoped>
@import url(@/css/components/textfield.css);
</style>
