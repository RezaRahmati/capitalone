import { Injectable } from '@angular/core';
import { MenuItem } from './menu-item';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MenuService {

  constructor() { }

  getMenu(): Observable<Array<MenuItem>> {
    return of([{
      name: 'Classic Burger',
      price: 15.99,
      description: 'mushroom & lentil patty, in-house smoked portobello, queso, magic sauce, lettuce, tomato, pickles'
    },
    {
      name: 'Crab Cake Burger',
      price: 22.99,
      description: 'hearts of palm & celeriac patty, tartar, lettuce, pickles'
    },
    {
      name: 'Firehouse Burger',
      price: 18.30,
      description: 'mushroom & lentil patty, pulled bbq jackfruit, jalapeño aïoli, queso, pickled jalapeño, lettuce, tomato'
    },
    {
      name: 'The Meatball',
      price: 10.50,
      description: 'farro & roasted mushrooms “meatballs”, basil olive pesto, tomato sauce, truffle almond parmesan, garlic aïoli'
    }
    ]);
  }
}
