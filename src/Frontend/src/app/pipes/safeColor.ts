import { Pipe } from '@angular/core';

@Pipe({name: 'safeColor'})
export class safeColor {
  constructor(){}

  transform(color) {
   debugger;
    return { 'background-color' : color};
  }
}