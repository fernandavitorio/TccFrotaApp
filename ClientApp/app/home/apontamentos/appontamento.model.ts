interface Apontamento {
    id: number;
    dtAtualizacao: Date;
    aditionalInformation: number;
    setor: string;
    tipo: string;
    veiculoId: number;
    veiculoIdentificador: string;
    veiculoPlaca: string;
    motoristaId: number;
    motoristaNome: string;
    coletor1Id: number;
    coletor1Nome: number;
    coletor2Id: number;
    coletor2Nome: number;
    coletor3Id: number;
    coletor3Nome: number;
    apontamentoInicialId: number;
    emAberto: boolean;
    filhos: Apontamento[];
}
