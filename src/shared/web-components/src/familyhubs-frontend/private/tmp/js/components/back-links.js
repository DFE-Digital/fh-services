import { nodeListForEach } from './helpers';
export function initializeBackButtons() {
    // Check if the page wasn't opened in a new tab or a standalone window
    if (history.length > 1) {
        const backLinks = document.querySelectorAll(".fh-back-link");
        nodeListForEach(backLinks, (link) => {
            link.classList.add("fh-back-link-visible");
            // set the href to the last page in the history
            link.href = document.referrer;
        });
    }
}

//# sourceMappingURL=data:application/json;charset=utf8;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbImNvbXBvbmVudHMvYmFjay1saW5rcy50cyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiQUFBQSxPQUFPLEVBQUUsZUFBZSxFQUFFLE1BQU0sV0FBVyxDQUFBO0FBRTNDLE1BQU0sVUFBVSxxQkFBcUI7SUFFakMsc0VBQXNFO0lBQ3RFLElBQUksT0FBTyxDQUFDLE1BQU0sR0FBRyxDQUFDLEVBQUUsQ0FBQztRQUVyQixNQUFNLFNBQVMsR0FBRyxRQUFRLENBQUMsZ0JBQWdCLENBQUMsZUFBZSxDQUFDLENBQUM7UUFDN0QsZUFBZSxDQUFDLFNBQVMsRUFBRSxDQUFDLElBQXVCLEVBQUUsRUFBRTtZQUVuRCxJQUFJLENBQUMsU0FBUyxDQUFDLEdBQUcsQ0FBQyxzQkFBc0IsQ0FBQyxDQUFDO1lBRTNDLCtDQUErQztZQUMvQyxJQUFJLENBQUMsSUFBSSxHQUFHLFFBQVEsQ0FBQyxRQUFRLENBQUM7UUFDbEMsQ0FBQyxDQUFDLENBQUM7SUFDUCxDQUFDO0FBQ0wsQ0FBQyIsImZpbGUiOiJjb21wb25lbnRzL2JhY2stbGlua3MuanMiLCJzb3VyY2VzQ29udGVudCI6WyJpbXBvcnQgeyBub2RlTGlzdEZvckVhY2ggfSBmcm9tICcuL2hlbHBlcnMnXG5cbmV4cG9ydCBmdW5jdGlvbiBpbml0aWFsaXplQmFja0J1dHRvbnMoKTogdm9pZCB7XG5cbiAgICAvLyBDaGVjayBpZiB0aGUgcGFnZSB3YXNuJ3Qgb3BlbmVkIGluIGEgbmV3IHRhYiBvciBhIHN0YW5kYWxvbmUgd2luZG93XG4gICAgaWYgKGhpc3RvcnkubGVuZ3RoID4gMSkge1xuXG4gICAgICAgIGNvbnN0IGJhY2tMaW5rcyA9IGRvY3VtZW50LnF1ZXJ5U2VsZWN0b3JBbGwoXCIuZmgtYmFjay1saW5rXCIpO1xuICAgICAgICBub2RlTGlzdEZvckVhY2goYmFja0xpbmtzLCAobGluazogSFRNTEFuY2hvckVsZW1lbnQpID0+IHtcblxuICAgICAgICAgICAgbGluay5jbGFzc0xpc3QuYWRkKFwiZmgtYmFjay1saW5rLXZpc2libGVcIik7XG5cbiAgICAgICAgICAgIC8vIHNldCB0aGUgaHJlZiB0byB0aGUgbGFzdCBwYWdlIGluIHRoZSBoaXN0b3J5XG4gICAgICAgICAgICBsaW5rLmhyZWYgPSBkb2N1bWVudC5yZWZlcnJlcjtcbiAgICAgICAgfSk7XG4gICAgfVxufSJdfQ==
