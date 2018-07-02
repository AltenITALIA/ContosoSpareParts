import { Pipe } from '@angular/core';

@Pipe({name: 'safeColor'})
export class safeColor {
  constructor(){}

  transform(color) {
    return { 'background-color' : color};
  }
}