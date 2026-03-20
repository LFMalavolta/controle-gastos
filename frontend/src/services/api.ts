const API_URL = "http://localhost:5140/api";

export async function getPessoas() {
  const response = await fetch(`${API_URL}/Pessoa`);
  return response.json();
}

export async function createPessoa(pessoa: any) {
  const response = await fetch(`${API_URL}/pessoa`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(pessoa),
  });

  return response.json();
}

export async function getCategorias() {
  const response = await fetch(`${API_URL}/categoria`);
  return response.json();
}

export async function getRelatorioPessoas() {
  const response = await fetch(`${API_URL}/relatorio/pessoas`);
  return response.json();
}

export async function createTransacao(transacao: any) {
  const response = await fetch(`${API_URL}/transacao`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(transacao),
  });

    if (!response.ok) {
        const error = await response.json();
        throw new Error(error.mensagem);
    }

    return response.json();
}

export async function createCategoria(categoria: any) {
  const response = await fetch(`${API_URL}/categoria`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(categoria),
  });

    if (!response.ok) {
        const error = await response.json();
        throw new Error(error.mensagem);
    }

    return response.json();
}

export async function getRelatorioCategorias() {
  const response = await fetch(`${API_URL}/relatorio/categorias`);
  return response.json();
}