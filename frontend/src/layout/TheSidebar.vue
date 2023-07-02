<template>
    <div class="sidebar" :class="{ 'sidebar--small': this.isSmallSidebar }">
        <div class="sidebar__logo">
            <div class="sidebar__logo--img"></div>
            <div class="sidebar__logo--text">{{resource['vn-VI'].nameApp}}</div>
        </div>
        <div class="sidebar__items" >
            <MISASidebarItem router="/" icon="icon--home" arrow="icon--down--big" :label="resource['vn-VI'].overview"></MISASidebarItem>

            <MISASidebarItem
                router="/property"
                icon="icon--car"
                :label="resource['vn-VI'].propertyText"
                arrow="icon--down--big"
            ></MISASidebarItem>

            <MISASidebarItem router="/" icon="icon--estate" arrow="icon--down--big" :label="resource['vn-VI'].propertyHt" :title="resource['vn-VI'].propertyHtFull"></MISASidebarItem>

            <MISASidebarItem router="/" icon="icon--tool" arrow="icon--down--big" :label="resource['vn-VI'].tool"></MISASidebarItem>

            <MISASidebarItem router="/" icon="icon--menu" :label="resource['vn-VI'].menu"></MISASidebarItem>

            <MISASidebarItem router="/" icon="icon--find" arrow="icon--down--big" :label="resource['vn-VI'].search"></MISASidebarItem>

            <MISASidebarItem router="/" icon="icon--report" arrow="icon--down--big" :label="resource['vn-VI'].report"></MISASidebarItem>
        </div>
        <div class="sidebar__footer">
            <div class="sidebar__footer__icon--left" @click="setSmallSidebar"></div>
            <div class="sidebar__footer__icon--right" @click="setBigSidebar"></div>
        </div>
    </div>
</template>

<script scoped>
import MISASidebarItem from '@/components/MISASidebarItem.vue';
import {MISAResource} from '../common/resource';
export default {
    components: { MISASidebarItem },
    name: 'TheSidebar',
    data() {
        return {
            isSmallSidebar: false,
            resource: MISAResource
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
