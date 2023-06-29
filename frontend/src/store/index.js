import { createStore } from 'vuex'

// Create a new store instance.
const store = createStore({
  state () {
    return {
      maskElementShow: false
    }
  },
  mutations: {
    async toggleMaskElement (state) {
      state.maskElementShow = !state.maskElementShow;
      await setTimeout(()=> {
      state.maskElementShow = !state.maskElementShow;
      },1000)
    }
  }
})

export default store