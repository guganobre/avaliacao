import { inject, Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { Seguro } from '../models/seguro.model';
import { RelatorioMedias } from '../models/relatorio-medias.model';

@Injectable({
  providedIn: 'root'
})
export class SeguroService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = 'http://localhost:5000/api/seguro';
  
  private readonly seguros = signal<Seguro[]>([]);

  carregarSeguros(): Observable<Seguro[]> {
    return this.http.get<Seguro[]>(this.apiUrl).pipe(
      map((seguros) => {
        this.seguros.set(seguros);
        return seguros;
      })
    );
  }

  obterSeguros() {
    return this.seguros.asReadonly();
  }

  obterRelatorioMedias(): RelatorioMedias {
    const seguros = this.seguros();

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

  obterRelatorioMediasJson(): string {
    const relatorio = this.obterRelatorioMedias();
    return JSON.stringify(relatorio, null, 2);
  }
}

