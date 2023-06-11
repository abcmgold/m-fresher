<template>
    <div>
        <button :class = "['btn',individualClass]">{{label}}</button>
    </div>
</template>

<script>
export default {
    name: "MISAButton",
    props: {
        label: String,
        individualClass: String,
    },
}
</script>

<style scoped>
@import url(../css/components/button.css);
</style>