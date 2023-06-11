<template>
    <el-select
        ref="myComboBox"
        v-model="selectedPropertyTypeName"
        :class="{ 'el-select--error': error }"
        :isRequired="isRequired"
        v-on:blur="onBlurFunction"
        v-on:change="onChangeFunction"
        :placeholder="placeholder"
        v-on:focus="() => {
            if (this.$parent.hideErrorMessage) {
                this.$parent.hideErrorMessage();
            }
        }"
        filterable
        :value="modelValue"
        @change="
            $emit(
                'update:modelValue',
                this.dataSelect.find((code) => {
                    return code.value == this.selectedPropertyTypeName;
                }).label,
            )
        "
    >
        <el-option
            v-for="code in this.dataSelect"
            :key="code.value"
            :label="code.label"
            :value="code.value"
        ></el-option>
    </el-select>
</template>

<script scoped>
export default {
    data() {
        return { selectedPropertyTypeName: '', error: false };
    },
    name: 'MISACombobox',
    props: {
        dataSelect: Array,
        modelValue: String,
        isRequired: Boolean,
        placeholder: String,
        errorMsg: String,
        type: String,
    },
    mounted() {
        this.selectedPropertyTypeName = this.modelValue;
    },
    methods: {
        onBlurFunction() {
            if (this.isRequired) {
                if (this.modelValue === undefined || this.modelValue == '' || this.modelValue.trim().length == 0) {
                    this.error = true;
                    // this.$refs.inputValue.value = this.$refs.inputValue.value.trim();
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
    },
    watch: {},
    emit: ['update:modelValue'],
};
</script>

<style scoped>
@import url(@/css/components/combobox.css);
</style>
