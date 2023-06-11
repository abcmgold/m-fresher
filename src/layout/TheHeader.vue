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
          <li class="header__right-icon" title="Thông báo">
            <img src="../assets/icon/bell-icon.svg" />
          </li>
          <li class="header__right-icon">
            <img src="../assets/icon/system-icon.svg" />
          </li>
          <li class="header__right-icon" title="Hỏi đáp">
            <img src="../assets/icon/support-icon.svg" />
          </li>
          <li
            class="header__right-icon header__right-sub-icon"
            title="Thông tin cá nhân"
          >
            <img src="../assets/icon/user-icon.svg" />
            <div class="user__icon-down"></div>
          </li>
        </ul>
      </div>
    </div>
  </div>
</template>

<script scoped>
export default {
  name: "TheHeader",
};
</script>

<style>
@import url(@/css/elements/header.css);
</style>
