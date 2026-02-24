import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () => import('./components/relatorio-medias/relatorio-medias.component').then(m => m.RelatorioMediasComponent)
  }
];
