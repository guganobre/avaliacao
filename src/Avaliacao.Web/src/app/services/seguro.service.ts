import { inject, Injectable, signal } from '@angular/core';
import { Seguro } from '../models/seguro.model';
import { RelatorioMedias } from '../models/relatorio-medias.model';

@Injectable({
  providedIn: 'root'
})
export class SeguroService {
  private readonly MARGEM_SEGURANCA = 0.03; // 3%
  private readonly LUCRO = 0.05; // 5%

  // Dados mockados de seguros
  private readonly segurosMockados = signal<Seguro[]>([
    {
      id: '1',
      veiculo: { valor: 10000, marcaModelo: 'Honda Civic' },
      segurado: { nome: 'João Silva', cpf: '123.456.789-00', idade: 35 },
      taxaRisco: 0.025,
      premioRisco: 250,
      premioPuro: 257.5,
      premioComercial: 270.37,
      valorSeguro: 270.37,
      dataCalculo: new Date('2024-01-15')
    },
    {
      id: '2',
      veiculo: { valor: 25000, marcaModelo: 'Toyota Corolla' },
      segurado: { nome: 'Maria Santos', cpf: '987.654.321-00', idade: 28 },
      taxaRisco: 0.025,
      premioRisco: 625,
      premioPuro: 643.75,
      premioComercial: 675.94,
      valorSeguro: 675.94,
      dataCalculo: new Date('2024-01-20')
    },
    {
      id: '3',
      veiculo: { valor: 50000, marcaModelo: 'BMW 320i' },
      segurado: { nome: 'Pedro Oliveira', cpf: '456.789.123-00', idade: 42 },
      taxaRisco: 0.025,
      premioRisco: 1250,
      premioPuro: 1287.5,
      premioComercial: 1351.88,
      valorSeguro: 1351.88,
      dataCalculo: new Date('2024-02-01')
    },
    {
      id: '4',
      veiculo: { valor: 15000, marcaModelo: 'Volkswagen Gol' },
      segurado: { nome: 'Ana Costa', cpf: '789.123.456-00', idade: 31 },
      taxaRisco: 0.025,
      premioRisco: 375,
      premioPuro: 386.25,
      premioComercial: 405.56,
      valorSeguro: 405.56,
      dataCalculo: new Date('2024-02-10')
    },
    {
      id: '5',
      veiculo: { valor: 80000, marcaModelo: 'Mercedes-Benz C200' },
      segurado: { nome: 'Carlos Mendes', cpf: '321.654.987-00', idade: 50 },
      taxaRisco: 0.025,
      premioRisco: 2000,
      premioPuro: 2060,
      premioComercial: 2163,
      valorSeguro: 2163,
      dataCalculo: new Date('2024-02-15')
    }
  ]);

  /**
   * Calcula o seguro de um veículo
   */
  calcularSeguro(valorVeiculo: number): {
    taxaRisco: number;
    premioRisco: number;
    premioPuro: number;
    premioComercial: number;
    valorSeguro: number;
  } {
    const taxaRisco = (valorVeiculo * 5) / (2 * valorVeiculo);
    const premioRisco = taxaRisco * valorVeiculo;
    const premioPuro = premioRisco * (1 + this.MARGEM_SEGURANCA);
    const premioComercial = premioPuro * (1 + this.LUCRO);
    const valorSeguro = premioComercial;

    return {
      taxaRisco,
      premioRisco,
      premioPuro,
      premioComercial,
      valorSeguro
    };
  }

  /**
   * Retorna todos os seguros
   */
  obterSeguros() {
    return this.segurosMockados.asReadonly();
  }

  /**
   * Calcula as médias aritméticas dos seguros
   */
  obterRelatorioMedias(): RelatorioMedias {
    const seguros = this.segurosMockados();

    if (seguros.length === 0) {
      return {
        mediaTaxaRisco: 0,
        mediaPremioRisco: 0,
        mediaPremioPuro: 0,
        mediaPremioComercial: 0,
        mediaValorSeguro: 0,
        totalSeguros: 0
      };
    }

    const total = seguros.length;
    const somaTaxaRisco = seguros.reduce((acc, s) => acc + s.taxaRisco, 0);
    const somaPremioRisco = seguros.reduce((acc, s) => acc + s.premioRisco, 0);
    const somaPremioPuro = seguros.reduce((acc, s) => acc + s.premioPuro, 0);
    const somaPremioComercial = seguros.reduce((acc, s) => acc + s.premioComercial, 0);
    const somaValorSeguro = seguros.reduce((acc, s) => acc + s.valorSeguro, 0);

    return {
      mediaTaxaRisco: somaTaxaRisco / total,
      mediaPremioRisco: somaPremioRisco / total,
      mediaPremioPuro: somaPremioPuro / total,
      mediaPremioComercial: somaPremioComercial / total,
      mediaValorSeguro: somaValorSeguro / total,
      totalSeguros: total
    };
  }

  /**
   * Retorna o relatório em formato JSON
   */
  obterRelatorioMediasJson(): string {
    const relatorio = this.obterRelatorioMedias();
    return JSON.stringify(relatorio, null, 2);
  }
}

