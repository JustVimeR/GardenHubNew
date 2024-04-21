

export interface Project {
  id: number;
  title: string;
  location: string;
  budget: number;
  description: string;
  numberOfRequriedGardeners: number;
  status: string;
  startDate: string;
  endDate: string;
  workTypes: { id: number }[];
}
