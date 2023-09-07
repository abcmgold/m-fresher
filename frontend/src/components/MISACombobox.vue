<template>
    <el-select
        ref="myComboBox"
        v-model="selectedPropertyTypeName"
        :class="{ 'el-select--error': error }"
        class="combo-box"
        :isRequired="isRequired"
        v-on:blur="onBlurFunction"
        v-on:change="onChangeFunction"
        :placeholder="placeholder"
        :filterable="this.filterable"
        :value="modelValue"
        :no-match-text="'Không có kết quả'"
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
            class="select"
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
        filterable: Boolean,
    },
    mounted() {
        this.selectedPropertyTypeName = this.modelValue;
    },
    watch: {},
    methods: {
        /*
         * Sự kiện khi blur khỏi ô input
         * Author: BATUAN (14/06/2023)
         */
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
        /*
         * Sự kiện khi ô input thay đổi
         * Author: BATUAN (14/06/2023)
         */
        onChangeFunction() {
            this.error = false;
            if (this.$parent.hideErrorMessage) {
                this.$parent.hideErrorMessage();
            }
        },
        /*
         * Sự kiện tự động focus vào ô input
         * Author: BATUAN (14/06/2023)
         */
        autoFocus() {
            this.$refs.myComboBox.focus();
            // this.$refs.myComboBox.blur();
        },
        /*
         * Sự kiện tự động blur vào ô input
         * Author: BATUAN (14/06/2023)
         */
         autoBlur() {
            // this.$refs.myComboBox.focus();
            this.$refs.myComboBox.blur();
        },
    },
    emits: ['update:modelValue'],
};
</script>

<style scoped>
@import url(@/css/components/combobox.css);
</style>
