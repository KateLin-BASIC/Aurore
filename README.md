# Aurore.ga
ASP.Net (MVC+Webform) 기반 URL 단축기
## DB 구조
```SQL
CREATE TABLE url (
 _id INT PRIMARY KEY AUTO_INCREMENT,
 url TEXT NOT NULL,
 shorted TEXT NOT NULL 
) ENGINE=INNODB;
```
## 직접 사용해보기
https://aurore.ga
