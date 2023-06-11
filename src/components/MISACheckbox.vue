<template>
    <div>
        <input type="checkbox" v-model="isChecked" @change="handleCheckboxChange" />
    </div>
</template>

<script scoped>
export default {
    name: 'MISACheckbox',
    props: {
        checkedCheckbox: Function,
        uncheckedCheckbox: Function,
        selectAllRow: Function,
        unSelectAllRow: Function,
        index: Number,
        type: String,
    },
    data() {
        return {
            isChecked: false,
        };
    },
    methods: {
        handleCheckboxChange() {
            // Xử lý khi checkbox được chọn

            if (this.isChecked) {
                if (this.type == 'primary') {
                    this.$emit('selectAllRow');
                } else {
                    this.$emit('checkedCheckbox', this.index);
                }
            } else {
                // Xử lý khi checkbox bị bỏ chọn

                if (this.type == 'primary') {
                    this.$emit('unSelectAllRow');
                } else {
                    this.$emit('uncheckedCheckbox', this.index);
                }
            }
        },
    },
};
</script>

<style scoped>
@import url(../css/components/table.css);
</style>
