<template>
    <div class="pagination-block">
        <el-pagination
            layout="prev, pager, next"
            :total="this.numberPages"
            :pager-count="5"
            v-model:current-page="this.currentPage"       
            @current-change="handleCurrentChange(this.currentPage)"
        />
    </div>
</template>

<script scoped>
export default {
    name: 'MISAPagination',
    props: {
        numberPages: Number
    },
    data() {
        return {
            currentPage: 1,
        };
    },
    methods: {
        handleCurrentChange() {
            this.$emit('changeCurrentPage', this.currentPage)
        },
    },
};
</script>

<style>
.el-pager .number {
    width: 24px !important;
    height: 24px !important;
    margin: 0 4px;
    padding: 0px !important;
    line-height: 24px;
}

.el-pager li {
    min-width: 24px !important;
}

.more {
    width: 24px !important;
}
</style>
