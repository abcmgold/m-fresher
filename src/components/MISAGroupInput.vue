<template>
    <div class="input-group">
        <div class="input__text--label">
            {{ text }}
            <div class="input__icon--force" v-show="isForce">*</div>
        </div>
        <slot @showErrorMessage="showErrorMessage"></slot>
        <m-validate-text
            ref="validateText"
            :isEmptyField="this.isShowMessage"
            :message="this.message"
        ></m-validate-text>
    </div>
</template>

<script>
export default {
    name: 'MISAGroupInput',
    props: {
        text: String,
        isForce: Boolean,
        message: String,
    },
    data() {
        return {
            isShowMessage: false,
        };
    },
    mounted() {
        console.log(this.$refs);
    },
    methods: {
        showErrorMessage() {
            this.isShowMessage = true;
        },
        hideErrorMessage() {
            this.isShowMessage = false;
        },
    },
};
</script>

<style scoped>
@import url(@/css/components/inputgroup.css);
</style>
