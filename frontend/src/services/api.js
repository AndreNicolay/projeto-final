const API = import.meta.env.VITE_API_URL

async function pedir(caminho, opcoes = {}) {
  const resposta = await fetch(`${API}${caminho}`, {
    headers: { 'Content-Type': 'application/json' },
    ...opcoes,
  })
  if (!resposta.ok) {
    throw new Error(`A API respondeu com erro ${resposta.status}`)
  }
  return resposta.status === 204 ? null : resposta.json()
}

export const listarProdutos = () => pedir('/api/produtos')

export const buscarProduto = (id) => pedir(`/api/produtos/${id}`)

export const criarProduto = (produto) =>
  pedir('/api/produtos', { method: 'POST', body: JSON.stringify(produto) })

export const atualizarProduto = (id, produto) =>
  pedir(`/api/produtos/${id}`, { method: 'PUT', body: JSON.stringify(produto) })

export const excluirProduto = (id) =>
  pedir(`/api/produtos/${id}`, { method: 'DELETE' })
