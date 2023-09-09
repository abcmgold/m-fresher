import {createStore} from 'vuex'

// Create a new store instance.
const store = createStore({
    state() {
        return {
            isShowPaging: true,
            isShowSummary: true,
            maskElementShow: false,
            contextMenuShow: false,
            contextMenuSize: {
                x: 0,
                y: 0
            }
        }
    },
    mutations: {
        toggleMaskElementShow(state) {
            state.maskElementShow = !state.maskElementShow;
        },
        toggleShowPaging(state) {
            state.isShowPaging = !state.isShowPaging;
        },
        toggleShowSummary(state) {
            state.isShowSummary = !state.isShowSummary;
        },
        showContextMenu(state) {
            state.contextMenuShow = true;
        },
        hideContextMenu(state) {
            state.contextMenuShow = false;
        },
        setContextMenuSize(state, {x, y}) {
            state.contextMenuSize.x = x;
            state.contextMenuSize.y = y;
        }
    },

    getters: {
        getMaskElementShow(state) {
            return state.maskElementShow;
        },
        getContextMenuSize(state) {
            return state.contextMenuSize;
        },
        getIsShowContextMenu(state) {
            return state.contextMenuShow;
        },
        getIsShowPaging(state) {
            return state.isShowPaging;
        },
        getIsShowSummary(state) {
            return state.isShowSummary;
        }
    }
})

export default store
