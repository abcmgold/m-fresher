<template>
    <div class="setting-menu" v-clickoutside="toggleSetting">
        <div class="setting-menu__header">
            <div class="menu__title">{{ this.$_MISAResource['vn-VI'].hide }}</div>
            <div
                class="menu__close icon--close"
                :content="this.$_MISAResource['vn-VI'].close"
                v-tippy="{ placement: 'top' }"
                @click="this.$emit('toggleMiniSetting')"
            ></div>
        </div>
        <div class="setting-menu__separate"></div>
        <!-- <m-text-input
            v-model="this.searchInput"
            :placeholder="this.$_MISAResource['vn-VI'].find"
            individualClass="text__input-icon-start"
        ></m-text-input> -->
        <ul class="menu__items">
            <li class="menu__item">
                <span>Phân trang</span>
                <m-checkbox
                    ref="sumaryPaging"
                    type="setting"
                    :storeFunction="() => this.$store.commit('toggleShowPaging')"
                ></m-checkbox>
            </li>
            <li class="menu__item">
                <span>Summary</span>
                <m-checkbox
                    ref="sumaryCheckbox"
                    type="setting"
                    :storeFunction="() => this.$store.commit('toggleShowSummary')"
                ></m-checkbox>
            </li>
        </ul>
    </div>
</template>

<script>
export default {
    name: 'MISASetting',
    data() {
        return {
        };
    },
    props: {
        isShowMiniSetting: Boolean,
    },
    mounted() {
        for (var ref in this.$refs) {
            this.$refs[ref].isChecked = true;
        }
    },
    methods: {
        toggleSetting() {
            if (this.isShowMiniSetting) {
                this.$emit('toggleMiniSetting')
            }
        }
    }
};
</script>

<style scoped>
@import url(../css/components/setting.css);
@import url(../css/components/icon.css);
</style>
