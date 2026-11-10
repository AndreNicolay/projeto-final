<script setup>
import { ref, onMounted } from 'vue'
import {
  listarProdutos, criarProduto, excluirProduto,
} from '@/services/api'

const produtos = ref([])
const novo = ref({ nome: '', preco: 0 })
const erro = ref('')

// formata 19.9 como R$ 19,90
const moeda = (valor) =>
  Number(valor).toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' })

async function carregar() {
  try {
    erro.value = ''
    produtos.value = await listarProdutos()
  } catch {
    erro.value = 'Erro ao carregar. A API está rodando?'
  }
}

async function salvar() {
  try {
    await criarProduto(novo.value)
    novo.value = { nome: '', preco: 0 }
    await carregar()
  } catch {
    erro.value = 'Erro ao salvar o produto.'
  }
}

async function excluir(id) {
  try {
    await excluirProduto(id)
    await carregar()
  } catch {
    erro.value = 'Erro ao excluir o produto.'
  }
}

// quando a tela abre
onMounted(carregar)
</script>

<template>
  <h1>Produtos</h1>

  <form @submit.prevent="salvar">
    <input v-model="novo.nome" placeholder="Nome" required />
    <input v-model.number="novo.preco" type="number"
           step="0.01" min="0" required />
    <button type="submit">Adicionar</button>
  </form>

  <p v-if="erro" style="color: crimson">{{ erro }}</p>

  <ul>
    <li v-for="p in produtos" :key="p.id">
      {{ p.nome }}: {{ moeda(p.preco) }}
      <button @click="excluir(p.id)">Excluir</button>
    </li>
  </ul>
</template>
