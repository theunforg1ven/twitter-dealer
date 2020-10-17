import { Component, OnInit } from '@angular/core';

const LINE_CHART_SAMPLE_DATA: any[] = [
  { data: [32, 14, 46, 23, 38, 56], label: 'Sentiment Analysis'},
  { data: [12, 18, 26, 13, 28, 26], label: 'Image Recognition'},
  { data: [52, 34, 49, 53, 68, 62], label: 'Forecasting'},
];
const LINE_CHART_LABELS: string[] = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun'];

@Component({
  selector: 'app-line-chart',
  templateUrl: './line-chart.component.html',
  styleUrls: ['./line-chart.component.scss']
})
export class LineChartComponent implements OnInit {

  lineChartData: any = LINE_CHART_SAMPLE_DATA;
  lineChartLabels: any = LINE_CHART_LABELS;
  lineChartOptions: any = {
    responsive: true
  };
  lineChartType = 'line';

  constructor() { }

  ngOnInit(): void {
  }

}
