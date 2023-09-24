const pressEscEvent = {
    beforeMount(el, binding) {
        el.pressEsc = event => {
            if (event.key === 'Escape') {
                binding.value()
            }
        }
        document.addEventListener('keydown', el.pressEsc)
    },
    unmounted(el) {
        if (el.pressEsc) {
            document.removeEventListener('keydown', el.pressEsc);
            delete el.pressEsc;
        }
    }
}

const leftClickMouse = {
    mounted(el, binding) {
        el.leftClickHandler = event => {
            if (event.button === 0) { // Kiểm tra nút chuột là nút trái (0)
                binding.instance.$store.commit('hideContextMenu');
            }
        };
        el.addEventListener('click', el.leftClickHandler);
    },
    unmounted(el) {
        if (el.leftClickHandler) {
            el.removeEventListener('click', el.leftClickHandler);
            delete el.leftClickHandler;
        }
    }
};

const clickOutside = {
    mounted(el, binding) { // Lấy giá trị ràng buộc function từ directive
        const onClickOutside = binding.value;
        // Function để kiểm tra xem sự kiện click có diễn ra bên ngoài phần tử không
        el.onClickHandler = event => {
            if ((el !== event.target && !el.contains(event.target))) { // Gọi function truyền vào thông qua giá trị ràng buộc
                onClickOutside();
            }
        };
        // Thêm sự kiện click vào document.body để kiểm tra click bên ngoài phần tử
        document.addEventListener('click', el.onClickHandler);
    },
    unmounted(el) { // Xóa sự kiện khi directive unmounted
        if (el.onClickHandler) {
            document.removeEventListener('click', el.onClickHandler);
            delete el.onClickHandler;
        }
    }
};

const scrollOutside = {
    mounted(el, binding) { // Lấy giá trị ràng buộc function từ directive
        const onScrollOutside = binding.value;
        // Function để kiểm tra xem sự kiện click có diễn ra bên ngoài phần tử không
        el.onScrollHandler = event => {
            if ((!el.contains(event.target))) { // Gọi function truyền vào thông qua giá trị ràng buộc
                onScrollOutside();
            }
        };

        // Thêm sự kiện click vào document.body để kiểm tra click bên ngoài phần tử
        document.addEventListener('scroll', el.onScrollHandler);
    },
    unmounted(el) { // Xóa sự kiện khi directive unmounted
        if (el.onScrollHandler) {
            document.removeEventListener('scroll', el.onScrollHandler);
            delete el.onScrollHandler;
        }
    }
};

// Định nghĩa directive Vue.js
const resizeColumnDirective = {
    mounted(el) {
      let startX;
      let startWidth;
      const RESIZE_THRESHOLD = 5; // Adjust this value to change the cursor detection area width
  
      el.addEventListener("mousedown", (e) => {
        if (el.style.cursor === "col-resize") {
          startX = e.clientX;
          startWidth = el.clientWidth;
  
          document.addEventListener("mousemove", resizeColumn);
          document.addEventListener("mouseup", stopResize);
        }
      });
  
      el.addEventListener("mousemove", updateCursor);
  
      function updateCursor(e) {
        const offset = el.getBoundingClientRect().right - e.clientX;
  
        if (offset < RESIZE_THRESHOLD) {
          el.style.cursor = "col-resize";
        } else {
          el.style.cursor = "auto";
        }
      }
  
      function resizeColumn(e) {
        const width = startWidth + (e.clientX - startX);
        el.style.minWidth = `${width}px`;
      }
  
      function stopResize() {
        el.style.cursor = "auto";
        document.removeEventListener("mousemove", resizeColumn);
        document.removeEventListener("mouseup", stopResize);
      }
    },
  };
  
  const resizeRowDirective = {
    mounted(el) {
      let startY;
      let startHeight;
      const RESIZE_THRESHOLD = 5; // Adjust this value to change the cursor detection area height
  
      el.addEventListener("mousedown", (e) => {
        if (el.style.cursor === "row-resize") {
          startY = e.clientY;
          startHeight = el.clientHeight;
  
          document.addEventListener("mousemove", resizeRow);
          document.addEventListener("mouseup", stopResize);
        }
      });
  
      el.addEventListener("mousemove", updateCursor);
  
      function updateCursor(e) {
        const offset = el.getBoundingClientRect().bottom - e.clientY;
  
        if (offset < RESIZE_THRESHOLD) {
          el.style.cursor = "row-resize";
        } else {
          el.style.cursor = "auto";
        }
      }
  
      function resizeRow(e) {
        const height = startHeight + (e.clientY - startY);
        el.style.minHeight = `${height}px`;
      }
  
      function stopResize() {
        el.style.cursor = "auto";
        document.removeEventListener("mousemove", resizeRow);
        document.removeEventListener("mouseup", stopResize);
      }
    },
  };

  


export {
    clickOutside,
    pressEscEvent,
    leftClickMouse,
    scrollOutside,
    resizeColumnDirective,
    resizeRowDirective
};
