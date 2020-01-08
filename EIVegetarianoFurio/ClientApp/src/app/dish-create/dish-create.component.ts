import { Component, OnInit } from '@angular/core';
import { Dish } from '../dish/dish';
import { Category } from '../category/category';
import { CategoryService } from '../category/category.service';
import { DishService } from '../dish/dish.service';
import { Location } from '@angular/common';

@Component({
  selector: 'app-dish-create',
  templateUrl: './dish-create.component.html',
  styleUrls: ['./dish-create.component.css']
})
export class DishCreateComponent implements OnInit {
  dish: Dish = {} as Dish;
  cats: Category[] = [];

  constructor(private categoryService: CategoryService, private dishService: DishService, private location: Location) { }

  ngOnInit() {
    this.categoryService.getCategories().subscribe(x => this.cats = x);
  }

  saveDish() {
    this.dishService.createDish(this.dish).subscribe(() => this.location.back());
  }

  back() { this.location.back(); }



}
