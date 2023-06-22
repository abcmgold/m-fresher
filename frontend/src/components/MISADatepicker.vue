<template>
    <VueDatePicker
        ref="myDatePicker"
        v-model="modelValue"
        format="dd/MM/yyyy"
        auto-apply
        text-input
        :state="this.notError"
        @blur="onBlurFunction"
        @focus="onFocusFunction"
    ></VueDatePicker>
</template>

<script scoped>
import VueDatePicker from '@vuepic/vue-datepicker';
import '@vuepic/vue-datepicker/dist/main.css';
export default {
    name: 'MISADatepicker',
    props: {
        type: String,
        isRequired: {
            type: Boolean,
            default: false,
        },
        errorMsg: String,
    },
    data() {
        return {
            notError: true,
            modelValue: new Date().toLocaleDateString(),
        };
    },
    components: {
        VueDatePicker,
    },
    watch: {
        modelValue: function () {
            this.notError = true;
        },
    },
    methods: {
        onBlurFunction() {
            if (!this.modelValue) {
                this.notError = false;
                // if (this.$parent) {
                //     this.$parent.showErrorMessage();
                // }
            }
        },
        onFocusFunction() {
            this.notError = true;
            // if (this.$parent) {
            //     this.$parent.hideErrorMessage();
            // }
        },
    },
};
</script>

<style scoped>
@import url(../css/datepicker/datepicker.css);
</style>
