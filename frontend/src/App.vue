<template>
    <div class="container" v-leftclick="this.$store.commit('hideContextMenu')">
        <TheSidebar></TheSidebar>
        <div class="main">
            <TheHeader></TheHeader>
            <TheContent></TheContent>
        </div>
    </div>
    <div v-if="this.$store.getters.getMaskElementShow" class="mask-element"></div>
</template>

<script>
import TheHeader from './layout/TheHeader.vue';
import TheSidebar from './layout/TheSidebar.vue';
import TheContent from './layout/TheContent.vue';

export default {
    name: 'App',
    components: {
        TheHeader,
        TheSidebar,
        TheContent,
    },
};
</script>

<style>
@import url(@/css/main.css);
@import url(@/css/variables.css);   
</style>
