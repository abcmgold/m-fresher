<template>
    <div class="dialog" ref="dialog" tabindex="0">
        <div class="dialog__discription">
            <div class="dialog__discription--icon">
                <div class="icon--warning"></div>
            </div>
            <div class="dialog__discription--text" v-html="text"></div>
        </div>
        <div class="dialog__info" v-if="this.documentInfo && this.documentInfo.length">
            <div class="detail-btn">
                <div
                    class="detail-text"
                    v-if="this.documentInfo && this.documentInfo.length && this.isShowDetailText"
                    @click="toggleDocumentInfo"
                >
                    Xem chi tiết phát sinh
                </div>
                <div
                    class="detail-text"
                    v-if="this.documentInfo && this.documentInfo.length && !this.isShowDetailText"
                    @click="toggleDocumentInfo"
                >
                    Ẩn chi tiết phát sinh
                </div>
            </div>
            <div class="detail__info" v-if="this.documentInfo && this.documentInfo.length && !this.isShowDetailText">
                <div class="detail__info--text" v-for="info in this.documentInfo" :key="info" v-html="info"></div>
            </div>
        </div>
        <div class="dialog__list-btns">
            <div class="dialog__btn">
                <button
                    class="btn btn--primary"
                    ref="thirdBtn"
                    @click="dialogActions.thirdBtnFunction"
                    v-if="dialogActions.thirdDialogBtnText"
                >
                    {{ dialogActions.thirdDialogBtnText }}
                </button>
                <button
                    class="btn btn--noborder"
                    ref="secondBtn"
                    @click="dialogActions.secondBtnFunction"
                    v-if="dialogActions.secondDialogBtnText"
                >
                    {{ dialogActions.secondDialogBtnText }}
                </button>
                <button
                    class="btn btn--sub"
                    ref="firstBtn"
                    @click="dialogActions.firstBtnFunction"
                    v-if="dialogActions.firstDialogBtnText"
                >
                    {{ dialogActions.firstDialogBtnText }}
                </button>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    name: 'MISADialog',
    props: {
        text: String,
        firstBtnLabel: String,
        secondBtnLabel: String,
        thirdBtnLabel: String,
        firstBtnFunction: Function,
        secondBtnFunction: Function,
        thirdBtnFunction: Function,
        documentInfo: Array,
        dialogActions: Object,
        errorField: String,
        componentFocusedName: String,
    },
    methods: {
        toggleDocumentInfo() {
            this.isShowDetailText = !this.isShowDetailText;
        },
    },
    mounted() {
        this.$refs.thirdBtn.focus();
    },
    unmounted() {
        if (this.errorField) {
            if (this.$parent.$parent && this.$parent.$parent.$refs[this.errorField]) {
                this.$parent.$parent.$refs[this.errorField].autoFocus();
            } else {
                this.$parent.$parent.$refs[this.componentFocusedName].$refs[this.errorField].autoFocus();
            }
        }
    },
    data() {
        return {
            isShowDetailText: true,
        };
    },
};
</script>

<style>
@import url(../css/components/button.css);
@import url(../css/components/icon.css);
</style>
