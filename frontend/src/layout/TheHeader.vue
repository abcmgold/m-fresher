<template>
    <div class="header">
        <div class="header__left">
            <div class="header-title">Danh sách tài sản</div>
        </div>
        <div class="header__right">
            <div class="header__right-content">
                <div class="header__right-content-text">Sở tài chính</div>
                <div class="header__right-content-select">
                    <span class="header__right-content-select-label">Năm</span>
                    <input
                        type="number"
                        min="1900"
                        max="2100"
                        step="1"
                        value="2023"
                        class="header__right-content-select-input"
                        readonly=""
                    />
                    <div class="header__right-content-select-icon-up"></div>
                    <div class="header__right-content-select-icon-down"></div>
                </div>
            </div>
            <div class="header__right-icons">
                <ul class="header__right-list">
                    <el-tooltip effect="dark" content="Thông báo" placement="bottom-start">
                        <li class="header__right-icon">
                            <img src="../assets/icon/bell-icon.svg" />
                        </li>
                    </el-tooltip>
                    <el-tooltip effect="dark" content="Cài đặt" placement="bottom-start">
                        <li class="header__right-icon">
                            <img src="../assets/icon/system-icon.svg" />
                        </li>
                    </el-tooltip>
                    <el-tooltip effect="dark" content="Hỏi đáp" placement="bottom-start">
                        <li class="header__right-icon">
                            <img src="../assets/icon/support-icon.svg" />
                        </li>
                    </el-tooltip>
                    <el-tooltip effect="dark" content="Thông tin cá nhân" placement="bottom-start">
                        <li class="header__right-icon header__right-sub-icon">
                            <img src="../assets/icon/user-icon.svg" />
                            <div class="user__icon-down"></div>
                        </li>
                    </el-tooltip>
                </ul>
            </div>
        </div>
    </div>
</template>

<script scoped>
export default {
    name: 'TheHeader',
};
</script>

<style>
@import url(@/css/elements/header.css);
</style>
