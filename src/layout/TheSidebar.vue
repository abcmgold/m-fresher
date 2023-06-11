<template>
    <div class="sidebar" :class="{ 'sidebar--small': this.isSmallSidebar }">
        <div class="sidebar__logo">
            <div class="sidebar__logo--img"></div>
            <div class="sidebar__logo--text">MISA QLTS</div>
        </div>
        <div class="sidebar__items" >
            <MISASidebarItem router="/" icon="icon--home" arrow="icon--down--big" label="Tổng quan"></MISASidebarItem>

            <MISASidebarItem
                router="/property"
                icon="icon--car"
                label="Tài sản"
                arrow="icon--down--big"
            ></MISASidebarItem>

            <MISASidebarItem router="/" icon="icon--estate" arrow="icon--down--big" label="Tài sản HT-ĐB" title="Tài sản hợp thành"></MISASidebarItem>

            <MISASidebarItem router="/" icon="icon--tool" arrow="icon--down--big" label="Công cụ dụng cụ"></MISASidebarItem>

            <MISASidebarItem router="/" icon="icon--menu" label="Danh mục"></MISASidebarItem>

            <MISASidebarItem router="/" icon="icon--find" arrow="icon--down--big" label="Tra cứu"></MISASidebarItem>

            <MISASidebarItem router="/" icon="icon--report" arrow="icon--down--big" label="Báo cáo"></MISASidebarItem>
        </div>
        <div class="sidebar__footer">
            <div class="sidebar__footer__icon--left" @click="setSmallSidebar"></div>
            <div class="sidebar__footer__icon--right" @click="setBigSidebar"></div>
        </div>
    </div>
</template>

<script scoped>
import MISASidebarItem from '@/components/MISASidebarItem.vue';
export default {
    components: { MISASidebarItem },
    name: 'TheSidebar',
    data() {
        return {
            isSmallSidebar: false
        }
    },
    methods: {
        setSmallSidebar() {
            this.isSmallSidebar = true
        },
        setBigSidebar() {
            this.isSmallSidebar = false
        }
    }
};
</script>

<style>
@import url(@/css/elements/sidebar.css);
</style>
