<template>
    <div class="paging-total">
        Tổng số:&nbsp;&nbsp;<b>{{ this.totalRecords }}</b> bản ghi
    </div>
    <div class="paging-list">
        <m-combo-box
            class="combo-box--small"
            :dataSelect="this.dataSelect"
            v-model="this.size"
        ></m-combo-box>
        <div class="pagination-block">
            <el-pagination
                @before-change="handleBeforeChange"
                layout="prev, pager, next, jumper"
                :total="this.numberPages"
                :pager-count="5"
                v-model:current-page="this.currentPage"
                @current-change="handleCurrentChange(this.currentPage)"
            />
        </div>
    </div>
</template>

<script scoped>
export default {
    name: 'MISAPagination',
    props: {
        numberPages: Number,
        dataSelect: Array,
        pageSize: Number,
        totalRecords: Number
    },
    data() {
        return {
            currentPage: 1,
            size: 0
        };
    },
    created() {
        this.size = this.pageSize;
    },
    watch: {
        size: function(newValue) {
            this.$emit('changePageSize', newValue);
        }
    },
    methods: {
        handleCurrentChange() {
            this.$emit('changeCurrentPage', this.currentPage);
        },
        handleBeforeChange(newPage) {
            if (newPage <= 0) {
                return false; // Ngăn chặn thay đổi trang khi giá trị là số âm
            }
            return true; // Cho phép thay đổi trang với giá trị hợp lệ
        },
    },
};
</script>

<style>
@import url(../css/components/paging.css);

</style>
