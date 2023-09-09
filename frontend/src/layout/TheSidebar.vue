<template>
    <div class="sidebar" :class="{ 'sidebar--small': this.isSmallSidebar }">
        <div class="sidebar__logo">
            <div class="sidebar__logo--img"></div>
            <div class="sidebar__logo--text">{{ resource['vn-VI'].nameApp }}</div>
        </div>
        <div class="sidebar__items">
            <MISASidebarItem
                router="/"
                icon="icon--home"
                :label="resource['vn-VI'].overview"
                :class="{ 'sidebar-selected': selectedItems.includes('overview') }"
                @click="handleItemClick('overview')"
            ></MISASidebarItem>

            <MISASidebarItem
                router="/property"
                icon="icon--car"
                :label="resource['vn-VI'].propertyText"
                :isShowPropertyMenu="this.isShowPropertyMenu"
                :class="{ 'sidebar-selected': selectedItems.includes('property') }"
                @click="
                    () => {
                        togglePropertyMenu();
                        handleItemClick('property');
                    }
                "
            >
            </MISASidebarItem>
            <ul class="sidenav-collapse" v-if="this.isShowPropertyMenu">
                <li
                    class="sidenav-item"
                    v-for="(item, index) in this.propertyMenuList"
                    :class="{ 'sidebar-child-selected': this.index == index }"
                    :key="item.name"
                    @click="handleClickItemsChild(index)"
                >
                    <router-link :to="item.link">
                        <div class="icon--point-to"></div>
                        {{ item.name }}
                    </router-link>
                </li>
            </ul>

            <MISASidebarItem
                router="/"
                icon="icon--estate"
                :label="resource['vn-VI'].propertyHt"
                :content="resource['vn-VI'].propertyHtFull"
                v-tippy="{ placement: 'bottom', theme: 'light-theme ' }"
                :class="{ 'sidebar-selected': selectedItems.includes('propertyHt') }"
                @click="handleItemClick('propertyHt')"
            ></MISASidebarItem>

            <MISASidebarItem
                router="/"
                icon="icon--tool"
                :label="resource['vn-VI'].tool"
                :class="{ 'sidebar-selected': selectedItems.includes('tool') }"
                @click="handleItemClick('tool')"
            ></MISASidebarItem>

            <MISASidebarItem
                router="/"
                icon="icon--menu"
                :label="resource['vn-VI'].menu"
                :class="{ 'sidebar-selected': selectedItems.includes('menu') }"
                @click="handleItemClick('menu')"
            ></MISASidebarItem>

            <MISASidebarItem
                router="/"
                icon="icon--find"
                :label="resource['vn-VI'].search"
                :class="{ 'sidebar-selected': selectedItems.includes('search') }"
                @click="handleItemClick('search')"
            ></MISASidebarItem>

            <MISASidebarItem
                router="/"
                icon="icon--report"
                :label="resource['vn-VI'].report"
                :class="{ 'sidebar-selected': selectedItems.includes('report') }"
                @click="handleItemClick('report')"
            ></MISASidebarItem>
        </div>
        <div class="sidebar__footer">
            <div class="sidebar__footer__icon--left" @click="setSmallSidebar"></div>
            <div class="sidebar__footer__icon--right" @click="setBigSidebar"></div>
        </div>
    </div>
</template>

<script scoped>
import MISASidebarItem from '@/components/MISASidebarItem.vue';
import { MISAResource } from '../common/resource';
export default {
    components: { MISASidebarItem },
    name: 'TheSidebar',
    data() {
        return {
            isSmallSidebar: false,
            resource: MISAResource,
            isShowPropertyMenu: true,
            propertyMenuList: [
                {
                    name: 'Ghi tăng',
                    link: '/',
                },
                {
                    name: 'Thay đổi thông tin',
                    link: '/',
                },
                {
                    name: 'Đánh giá lại',
                    link: '/',
                },
                {
                    name: 'Điều chuyển tài sản',
                    link: 'property-transfer',
                },
                {
                    name: 'Đề nghị xử lý',
                    link: '/',
                },
                {
                    name: 'Ghi giảm',
                    link: '/',
                }
            ],
            selectedItems: ['property'],
            index: -1,
        };
    },
    methods: {
        setSmallSidebar() {
            this.isSmallSidebar = true;
            this.isShowPropertyMenu = false;
        },
        setBigSidebar() {
            this.isSmallSidebar = false;
        },
        togglePropertyMenu() {
            this.isShowPropertyMenu = !this.isShowPropertyMenu;
        },
        handleItemClick(itemKey) {
            this.index = -1;
            this.selectedItems = [];
            if (this.selectedItems.includes(itemKey)) {
                this.isShowPropertyMenu = false;
            } else {
                this.selectedItems.push(itemKey);
            }
            // this.setBigSidebar = true;
        },
        handleClickItemsChild(index) {
            this.index = index;
        },
    },
};
</script>

<style>
@import url(@/css/elements/sidebar.css);
@import url(@/css/components/icon.css);
</style>
