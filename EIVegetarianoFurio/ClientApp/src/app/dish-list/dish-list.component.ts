import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CategoryService } from '../category/category.service';
import { Category } from '../category/category';

@Component({
  selector: 'app-dish-list',
  templateUrl: './dish-list.component.html',
  styleUrls: ['./dish-list.component.css']
})
export class DishListComponent implements OnInit {

  constructor(private route: ActivatedRoute, private catService: CategoryService) { }
  category: Category;
  ngOnInit() {
    const catId = +this.route.snapshot.paramMap.get('categoryId');
    this.catService.getCategory(catId).subscribe(c => this.category= c);

  }

}
