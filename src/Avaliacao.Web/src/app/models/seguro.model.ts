import { Segurado } from './segurado.model';
import { Veiculo } from './veiculo.model';

export interface Seguro {
  id: string;
  veiculo: Veiculo;
  segurado: Segurado;
  taxaRisco: number;
  premioRisco: number;
  premioPuro: number;
  premioComercial: number;
  valorSeguro: number;
  dataCalculo: Date;
}

