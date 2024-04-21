import { Component, EventEmitter, OnDestroy, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-successfull-order',
  templateUrl: './successfull-order.component.html',
  styleUrls: ['./successfull-order.component.scss']
})
export class SuccessfullOrderComponent implements OnInit {
  @Output() feedbackSubmitted = new EventEmitter<{text: string, rating: number}>();
  starsArray: number[] = [1, 2, 3, 4, 5];
  hoveredStarIndex: number | null = null;
  clickedStarIndex: number | null = null;
  starIcons: string[] = [];
  reviewText: string = '';
  starRating: number | null = null;

  constructor() {
    
  }
  
  ngOnInit(): void {
    this.applyStars();
    
  }
  
  applyStars() : void {
    this.starIcons = this.starsArray.map(() => '../../../assets/star.svg');
  }

  highlightStars(index: number): void {
    if (this.clickedStarIndex !== null) {
      this.hoveredStarIndex = index;
      for (let i = 0; i <= this.clickedStarIndex; i++) {
        this.starIcons[i] = '../../../assets/star-fill.svg';
      }
    } else {
      this.hoveredStarIndex = index;
      for (let i = 0; i <= index; i++) {
        this.starIcons[i] = '../../../assets/star-fill.svg';
      }
    }
  }

  resetStars(): void {
    for (let i = 0; i < this.starsArray.length; i++) {
      if (this.clickedStarIndex !== null && i <= this.clickedStarIndex) {
        this.starIcons[i] = '../../../assets/star-fill.svg';
      } else {
        this.starIcons[i] = '../../../assets/star.svg';
      }
    }
    this.hoveredStarIndex = null;
  }

  logStar(index: number): void {
    this.clickedStarIndex = index;
    this.highlightStars(index);
    this.starRating = index + 1;
  }

  onSubmit(): void {
    this.feedbackSubmitted.emit({text: this.reviewText, rating: this.starRating});
  }
}
