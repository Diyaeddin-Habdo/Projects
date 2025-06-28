import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import '../../Models/clsCashierTable.dart';
import 'TableOrdersScreen.dart';
import 'package:my_coma/Models/API.dart';

class CashierTablesScreen extends StatefulWidget {
  @override
  _CashierTablesScreenState createState() => _CashierTablesScreenState();
}


class _CashierTablesScreenState extends State<CashierTablesScreen> {
  List<clsCashierTable> tables = [];
  String message ="";

  @override void initState(){
    super.initState();
    fetchTables();
  }
  Future<List<clsCashierTable>> fetchTables() async {
    final response = await http.get(Uri.parse('${clsAPI.baseURL}/${clsAPI.TABLES}'));

    if (response.statusCode == 200) {
      List jsonResponse = json.decode(response.body);
      tables = jsonResponse.map((data) => clsCashierTable.fromJson(data)).toList();
    } else {
      setState(() {
      message = "Hiç bir masa bulunmamaktadır.";
    });
    }
    return tables;
  }


  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Masalar', style: TextStyle(fontWeight: FontWeight.bold)),
        backgroundColor: Colors.blueGrey, // Daha uyumlu bir renk
        elevation: 0,
      ),
      body: FutureBuilder<List<clsCashierTable>>(
        future: fetchTables(),
        builder: (context, snapshot) {
          if (snapshot.connectionState == ConnectionState.waiting) {
            return Center(child: CircularProgressIndicator());
          } else if (snapshot.hasError) {
            return Center(child: Text('Bir hata oluştu: ${snapshot.error}'));
          } else if (!snapshot.hasData || snapshot.data!.isEmpty) {
            return Center(child: Text('Hiç masa yok.'));
          } else {
            final tables = snapshot.data!;
            return GridView.builder(
              padding: EdgeInsets.all(16),
              gridDelegate: SliverGridDelegateWithFixedCrossAxisCount(
                crossAxisCount: 2, // Kaç sütun olacağını belirler
                childAspectRatio: 3 / 2, // Kartların boyut oranı
                crossAxisSpacing: 16,
                mainAxisSpacing: 16,
              ),
              itemCount: tables.length,
              itemBuilder: (context, index) {
                final table = tables[index];
                return GestureDetector(
                  onTap: () {
                    // Masa siparişleri ekranına yönlendirme
                    Navigator.push(
                      context,
                      MaterialPageRoute(
                        builder: (context) => TableOrdersScreen(tableId: table.id),
                      ),
                    );
                  },
                  child: Container(
                    decoration: BoxDecoration(
                      color: Colors.white, // Arka planı beyaz yaparak sade bir görünüm
                      borderRadius: BorderRadius.circular(16),
                      border: Border.all(color: Colors.blueGrey, width: 2), // Daha belirgin bir kenarlık
                      boxShadow: [
                        BoxShadow(
                          color: Colors.black12, // Hafif bir gölge
                          blurRadius: 8,
                          offset: Offset(0, 4),
                        ),
                      ],
                    ),
                    child: Center(
                      child: Column(
                        mainAxisAlignment: MainAxisAlignment.center,
                        children: [
                          Icon(
                            Icons.table_restaurant,
                            size: 50,
                            color: Colors.blueGrey, // İkon rengi gri-mavi
                          ),
                          SizedBox(height: 10),
                          Text(
                            'Masa ${table.no}',
                            style: TextStyle(
                              fontSize: 18,
                              color: Colors.blueGrey, // Yazı rengi de uyumlu olacak şekilde
                              fontWeight: FontWeight.bold,
                            ),
                          ),
                        ],
                      ),
                    ),
                  ),
                );
              },
            );
          }
        },
      ),
    );
  }
}




