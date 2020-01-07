import { Injectable } from '@angular/core';
import { Dish } from './dish';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class DishService {

  constructor(private http: HttpClient) { }
  dish: Dish;

  getDish(dishId: number): Observable<Dish> {
    return this.http.get<Dish>(`/api/dishes/${dishId}`);
  }
  updateDish(dishId: Dish): Observable<Dish> {
    return this.http.put<Dish>(`/api/dishes/${dishId}`,this.dish);
  }

}
