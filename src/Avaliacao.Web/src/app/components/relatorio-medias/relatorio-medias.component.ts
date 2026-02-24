import { Component, computed, inject, ChangeDetectionStrategy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SeguroService } from '../../services/seguro.service';
import { RelatorioMedias } from '../../models/relatorio-medias.model';
import { BaseChartDirective } from 'ng2-charts';
import { ChartConfiguration, ChartData, ChartType, registerables } from 'chart.js';
import Chart from 'chart.js/auto';

// Registrar todos os componentes do Chart.js
Chart.register(...registerables);

@Component({
  selector: 'app-relatorio-medias',
  imports: [CommonModule, BaseChartDirective],
  templateUrl: './relatorio-medias.component.html',
  styleUrl: './relatorio-medias.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class RelatorioMediasComponent {
  private readonly seguroService = inject(SeguroService);

  protected readonly relatorio = computed(() => this.seguroService.obterRelatorioMedias());
  protected readonly relatorioJson = computed(() => this.seguroService.obterRelatorioMediasJson());

  // Configuração do gráfico de barras
  protected readonly barChartType: ChartType = 'bar';
  protected readonly barChartOptions: ChartConfiguration['options'] = {
    responsive: true,
    maintainAspectRatio: false,
    plugins: {
      legend: {
        display: true,
        position: 'top'
      },
      title: {
        display: true,
        text: 'Comparação de Médias dos Prêmios',
        font: {
          size: 16,
          weight: 'bold'
        }
      },
      tooltip: {
        callbacks: {
          label: (context) => {
            const value = context.parsed.y;
            if (value === null || value === undefined) {
              return '';
            }
            return new Intl.NumberFormat('pt-BR', {
              style: 'currency',
              currency: 'BRL'
            }).format(value);
          }
        }
      }
    },
    scales: {
      y: {
        beginAtZero: true,
        ticks: {
          callback: (value) => {
            return new Intl.NumberFormat('pt-BR', {
              style: 'currency',
              currency: 'BRL',
              maximumFractionDigits: 0
            }).format(Number(value));
          }
        }
      }
    }
  };

  protected readonly barChartData = computed<ChartData<'bar'>>(() => {
    const dados = this.relatorio();
    return {
      labels: ['Prêmio de Risco', 'Prêmio Puro', 'Prêmio Comercial', 'Valor do Seguro'],
      datasets: [
        {
          label: 'Valores em R$',
          data: [
            dados.mediaPremioRisco,
            dados.mediaPremioPuro,
            dados.mediaPremioComercial,
            dados.mediaValorSeguro
          ],
          backgroundColor: [
            'rgba(102, 126, 234, 0.8)',
            'rgba(118, 75, 162, 0.8)',
            'rgba(240, 147, 251, 0.8)',
            'rgba(245, 87, 108, 0.8)'
          ],
          borderColor: [
            'rgba(102, 126, 234, 1)',
            'rgba(118, 75, 162, 1)',
            'rgba(240, 147, 251, 1)',
            'rgba(245, 87, 108, 1)'
          ],
          borderWidth: 2
        }
      ]
    };
  });

  // Configuração do gráfico de pizza
  protected readonly pieChartType: ChartType = 'pie';
  protected readonly pieChartOptions: ChartConfiguration['options'] = {
    responsive: true,
    maintainAspectRatio: false,
    plugins: {
      legend: {
        display: true,
        position: 'right'
      },
      title: {
        display: true,
        text: 'Distribuição dos Prêmios',
        font: {
          size: 16,
          weight: 'bold'
        }
      },
      tooltip: {
        callbacks: {
          label: (context) => {
            const label = context.label || '';
            const value = context.parsed;
            if (value === null || value === undefined) {
              return label;
            }
            const dataArray = context.dataset.data.filter((d): d is number => typeof d === 'number');
            const total = dataArray.reduce((a, b) => a + b, 0);
            if (total === 0) {
              return `${label}: ${new Intl.NumberFormat('pt-BR', {
                style: 'currency',
                currency: 'BRL'
              }).format(value)}`;
            }
            const percentage = ((value / total) * 100).toFixed(2);
            return `${label}: ${new Intl.NumberFormat('pt-BR', {
              style: 'currency',
              currency: 'BRL'
            }).format(value)} (${percentage}%)`;
          }
        }
      }
    }
  };

  protected readonly pieChartData = computed<ChartData<'pie'>>(() => {
    const dados = this.relatorio();
    return {
      labels: ['Prêmio de Risco', 'Prêmio Puro', 'Prêmio Comercial', 'Valor do Seguro'],
      datasets: [
        {
          data: [
            dados.mediaPremioRisco,
            dados.mediaPremioPuro,
            dados.mediaPremioComercial,
            dados.mediaValorSeguro
          ],
          backgroundColor: [
            'rgba(102, 126, 234, 0.8)',
            'rgba(118, 75, 162, 0.8)',
            'rgba(240, 147, 251, 0.8)',
            'rgba(245, 87, 108, 0.8)'
          ],
          borderColor: [
            'rgba(102, 126, 234, 1)',
            'rgba(118, 75, 162, 1)',
            'rgba(240, 147, 251, 1)',
            'rgba(245, 87, 108, 1)'
          ],
          borderWidth: 2
        }
      ]
    };
  });

  protected formatarMoeda(valor: number): string {
    return new Intl.NumberFormat('pt-BR', {
      style: 'currency',
      currency: 'BRL'
    }).format(valor);
  }

  protected formatarPercentual(valor: number): string {
    return new Intl.NumberFormat('pt-BR', {
      style: 'percent',
      minimumFractionDigits: 2,
      maximumFractionDigits: 2
    }).format(valor);
  }

  protected copiarJson(): void {
    const json = this.relatorioJson();
    navigator.clipboard.writeText(json).then(() => {
      alert('JSON copiado para a área de transferência!');
    }).catch(() => {
      alert('Erro ao copiar JSON');
    });
  }
}

