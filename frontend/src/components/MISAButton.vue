<template>
        <button :class="['btn', individualClass]" :disabled="this.isDisabled">
            <div v-if="icon" :class="[icon]"></div>
            <span v-if="label">{{ label }}</span>
        </button>
</template>

<script>
export default {
    name: 'MISAButton',
    props: {    
        label: String,
        individualClass: String,
        icon: String,
        isDisabled: Boolean,
    },
};
</script>

<style scoped>
@import url(../css/components/button.css);
@import url(../css/components/icon.css);
</style>
