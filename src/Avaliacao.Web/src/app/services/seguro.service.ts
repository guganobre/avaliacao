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
  private readonly relatorioMedias = signal<RelatorioMedias | null>(null);

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

  carregarRelatorioMedias(): Observable<RelatorioMedias> {
    return this.http.get<RelatorioMedias>(`${this.apiUrl}/relatorio/medias`).pipe(
      map((relatorio) => {
        this.relatorioMedias.set(relatorio);
        return relatorio;
      })
    );
  }

  obterRelatorioMedias(): RelatorioMedias | null {
    return this.relatorioMedias();
  }

  obterRelatorioMediasJson(): string {
    const relatorio = this.obterRelatorioMedias();
    if (!relatorio) {
      return JSON.stringify({}, null, 2);
    }
    return JSON.stringify(relatorio, null, 2);
  }
}

