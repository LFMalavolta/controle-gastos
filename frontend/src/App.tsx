import { useEffect, useState } from "react";
import { 
  getPessoas, 
  createPessoa, 
  getCategorias, 
  createTransacao,
  createCategoria,
  getRelatorioPessoas,
  getRelatorioCategorias 
} from "./services/api";

function App() {
  const [pessoas, setPessoas] = useState<any[]>([]);
  const [nome, setNome] = useState("");
  const [idade, setIdade] = useState("");
  const [categorias, setCategorias] = useState<any[]>([]);
  const [descricao, setDescricao] = useState("");
  const [valor, setValor] = useState("");
  const [tipo, setTipo] = useState("0");
  const [pessoaId, setPessoaId] = useState("");
  const [categoriaId, setCategoriaId] = useState("");
  const [relatorio, setRelatorio] = useState<any>(null);
  const [descricaoCategoria, setDescricaoCategoria] = useState("");
  const [finalidade, setFinalidade] = useState("0");
  const [relatorioCategorias, setRelatorioCategorias] = useState<any>(null);

  useEffect(() => {
    loadPessoas();
    loadCategorias();
    loadRelatorio();
    loadRelatorioCategorias();
  }, []);

  async function loadPessoas() {
    const data = await getPessoas();
    setPessoas(data);
  }

  async function loadCategorias() {
    const data = await getCategorias();
    setCategorias(data);
  }

  async function loadRelatorio() {
    const data = await getRelatorioPessoas();
    setRelatorio(data);
  }

  async function loadRelatorioCategorias() {
    const data = await getRelatorioCategorias();
    setRelatorioCategorias(data);
  }

  async function handleAddPessoa() {
    if (!nome || !idade) {
      alert("Preencha todos os campos");
      return;
    }

    await createPessoa({
      nome: nome,
      idade: Number(idade),
    });

    setNome("");
    setIdade("");

    loadPessoas();

    alert("Pessoa criada com sucesso!");
  }

  async function handleAddTransacao() {
    if (!descricao || !valor || !pessoaId || !categoriaId) {
      alert("Preencha todos os campos");
      return;
    }

    await createTransacao({
      descricao,
      valor: Number(valor),
      tipo: Number(tipo),
      pessoaId: Number(pessoaId),
      categoriaId: Number(categoriaId),
    });
    setDescricao("");
    setValor("");
    setPessoaId("");
    setCategoriaId("");

    loadPessoas();
    loadRelatorio();
    loadRelatorioCategorias();

    alert("Transação criada com sucesso!");
  }

  async function handleAddCategoria() {
    if (!descricaoCategoria) {
      alert("Preencha a descrição");
      return;
    }

    await createCategoria({
      descricao: descricaoCategoria,
      finalidade: Number(finalidade),
    });

    setDescricaoCategoria("");
    setFinalidade("0");

    loadCategorias();
    loadRelatorioCategorias();

    alert("Categoria criada com sucesso!");
  }

  return (
    <div style={{ maxWidth: "600px", margin: "auto", fontFamily: "Arial" }}>
      <h1>Controle de Gastos</h1>

      <h2>Adicionar Pessoa</h2>

      <input
        type="text"
        placeholder="Nome"
        value={nome}
        onChange={(e) => setNome(e.target.value)}
      />

      <input
        type="number"
        placeholder="Idade"
        value={idade}
        onChange={(e) => setIdade(e.target.value)}
      />

      <button onClick={handleAddPessoa}>Adicionar</button>

      <h2>Pessoas</h2>

      <ul>
        {pessoas.map((p) => (
          <li key={p.id}>
            {p.nome} - {p.idade} anos
          </li>
        ))}
      </ul>

      <h2 style={{ marginTop: "20px" }}>Adicionar Categoria</h2>

      <input
        type="text"
        placeholder="Descrição"
        value={descricaoCategoria}
        onChange={(e) => setDescricaoCategoria(e.target.value)}
      />

      <select value={finalidade} onChange={(e) => setFinalidade(e.target.value)}>
        <option value="0">Despesa</option>
        <option value="1">Receita</option>
        <option value="2">Ambas</option>
      </select>

      <button onClick={handleAddCategoria}>Adicionar Categoria</button>

      <h3>Categorias</h3>

      <ul>
        {categorias.map((c) => (
          <li key={c.id}>
            {c.descricao}
          </li>
        ))}
      </ul>

    <h2 style={{ marginTop: "20px" }}>Adicionar Transação</h2>

    <input
      type="text"
      placeholder="Descrição"
      value={descricao}
      onChange={(e) => setDescricao(e.target.value)}
    />

    <input
      type="number"
      placeholder="Valor"
      value={valor}
      onChange={(e) => setValor(e.target.value)}
    />

    <select value={tipo} onChange={(e) => setTipo(e.target.value)}>
      <option value="0">Despesa</option>
      <option value="1">Receita</option>
    </select>

    <select value={pessoaId} onChange={(e) => setPessoaId(e.target.value)}>
      <option value="">Selecione pessoa</option>
      {pessoas.map((p) => (
        <option key={p.id} value={p.id}>
          {p.nome}
        </option>
      ))}
    </select>

    <select value={categoriaId} onChange={(e) => setCategoriaId(e.target.value)}>
      <option value="">Selecione categoria</option>
      {categorias.map((c) => (
        <option key={c.id} value={c.id}>
          {c.descricao}
        </option>
      ))}
    </select>

    <button onClick={handleAddTransacao}>Adicionar Transação</button>

    <h2>Relatório por Pessoa</h2>

    {relatorio && (
      <div style={{ marginBottom: "20px" }}>
        <ul>
          {relatorio.pessoas.map((p: any, index: number) => (
            <li key={p.pessoa}>
              <strong>{p.pessoa}</strong> | Receita: {p.totalReceitas} | Despesa: {p.totalDespesas} | Saldo: {p.saldo}
            </li>
          ))}
        </ul>

        <h3>Total Geral</h3>
        <p>
          Receita: {relatorio.totalGeral.totalReceitas} | Despesa: {relatorio.totalGeral.totalDespesas} | Saldo: {relatorio.totalGeral.saldo}
        </p>
      </div>
    )}

    <h2>Relatório por Categoria</h2>

    {relatorioCategorias && (
      <div style={{ marginBottom: "20px" }}>
        <ul>
          {relatorioCategorias.categorias.map((c: any, index: number) => (
            <li key={c.categoria}>
              <strong>{c.categoria}</strong> | Receita: {c.totalReceitas} | Despesa: {c.totalDespesas} | Saldo: {c.saldo}
            </li>
          ))}
        </ul>

        <h3>Total Geral</h3>
        <p>
          Receita: {relatorioCategorias.totalGeral.totalReceitas} | Despesa: {relatorioCategorias.totalGeral.totalDespesas} | Saldo: {relatorioCategorias.totalGeral.saldo}
        </p>
      </div>
    )}
    </div>
  );
}

export default App;