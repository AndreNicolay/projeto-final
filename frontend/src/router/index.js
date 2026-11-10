import { createRouter, createWebHistory } from 'vue-router'
import ProdutosView from '@/views/ProdutosView.vue'

export default createRouter({
  history: createWebHistory(),
  routes: [
    { path: '/', name: 'produtos', component: ProdutosView },
  ],
})
