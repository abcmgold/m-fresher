<template>
    <div class="container">
        <TheSidebar></TheSidebar>
        <div class="main">
            <div v-if="this.$store.state.maskElementShow" class="mask-element"></div>
            <TheHeader></TheHeader>
            <TheContent></TheContent>
        </div>
    </div>
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
