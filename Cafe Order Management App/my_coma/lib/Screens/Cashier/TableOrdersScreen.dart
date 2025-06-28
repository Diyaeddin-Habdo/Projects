import 'dart:convert';
import 'package:flutter/material.dart';
import 'package:http/http.dart' as http;
import 'package:my_coma/Models/API.dart';
import 'package:my_coma/Models/clsTableOrders.dart';

class TableOrdersScreen extends StatelessWidget {
  final int tableId;

  TableOrdersScreen({required this.tableId});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Masa Siparişleri', style: TextStyle(fontWeight: FontWeight.bold)),
        backgroundColor: Colors.blueGrey,
        elevation: 0,
      ),
      body: FutureBuilder<List<clsTableOrders>>(
        future: fetchOrders(),
        builder: (context, snapshot) {
          if (snapshot.connectionState == ConnectionState.waiting) {
            return Center(child: CircularProgressIndicator());
          } else if (snapshot.hasError) {
            return Center(
              child: Text('Bir hata oluştu: ${snapshot.error}'),
            );
          } else if (!snapshot.hasData || snapshot.data!.isEmpty) {
            return Center(
              child: Column(
                mainAxisAlignment: MainAxisAlignment.center,
                children: [
                  Icon(Icons.sentiment_dissatisfied, size: 80, color: Colors.grey),
                  SizedBox(height: 16),
                  Text(
                    'Bu masa için hiç sipariş verilmedi.',
                    style: TextStyle(fontSize: 18, color: Colors.grey),
                  ),
                ],
              ),
            );
          } else {
            final orders = snapshot.data!;
            double totalAmount = orders.fold(0, (sum, order) => sum + order.price);

            return Stack(
              children: [
                // Sipariş Listesi
                ListView.builder(
                  padding: EdgeInsets.all(16),
                  itemCount: orders.length,
                  itemBuilder: (context, index) {
                    final order = orders[index];
                    return Card(
                      margin: EdgeInsets.symmetric(vertical: 10),
                      shape: RoundedRectangleBorder(
                        borderRadius: BorderRadius.circular(15),
                      ),
                      elevation: 4,
                      child: ListTile(
                        contentPadding: EdgeInsets.symmetric(vertical: 10, horizontal: 16),
                        leading: Icon(Icons.fastfood, color: Colors.blueGrey, size: 40),
                        title: Text(
                          order.name,
                          style: TextStyle(fontSize: 18, fontWeight: FontWeight.bold),
                        ),
                        subtitle: Text(
                          'Fiyat: ${order.price} ₺',
                          style: TextStyle(color: Colors.grey[700]),
                        ),
                      ),
                    );
                  },
                ),
                // Toplam Tutarı Sol Alt Köşeye Yerleştirin
                Positioned(
                  left: 16,
                  bottom: 16,
                  child: Container(
                    padding: EdgeInsets.all(12),
                    decoration: BoxDecoration(
                      color: Colors.white,
                      borderRadius: BorderRadius.circular(10),
                      boxShadow: [
                        BoxShadow(
                          color: Colors.black26,
                          blurRadius: 4,
                          offset: Offset(0, 2),
                        ),
                      ],
                    ),
                    child: Text(
                      'Toplam Tutar: ${totalAmount.toStringAsFixed(2)} ₺',
                      style: TextStyle(fontSize: 20, fontWeight: FontWeight.bold),
                    ),
                  ),
                ),
              ],
            );
          }
        },
      ),
      floatingActionButton: FloatingActionButton.extended(
        onPressed: () => pay(context, tableId),
        label: Text('Ödeme Al', style: TextStyle(fontWeight: FontWeight.bold)),
        icon: Icon(Icons.payment),
        backgroundColor: Colors.blueGrey,
      ),
    );
  }

  Future<List<clsTableOrders>> fetchOrders() async {
    final response = await http.get(Uri.parse('${clsAPI.baseURL}/${clsAPI.ALL_TABLE_ORDERS}/$tableId'));

    if (response.statusCode == 200) {
      List jsonResponse = json.decode(response.body);
      return jsonResponse.map((data) => clsTableOrders.fromJson(data)).toList();
    } else if (response.statusCode == 404) {
      return [];
    } else {
      throw Exception('Siparişleri yükleme başarısız oldu: ${response.statusCode}');
    }
  }

  Future<void> pay(BuildContext context, int tableId) async {
    final response = await http.post(
      Uri.parse('${clsAPI.baseURL}/${clsAPI.PAY}/$tableId'),
    );

    if (response.statusCode == 200) {
      ScaffoldMessenger.of(context).showSnackBar(SnackBar(
        content: Text('Ödeme başarıyla alındı.'),
        backgroundColor: Colors.green,
      ));
    } else {
      ScaffoldMessenger.of(context).showSnackBar(SnackBar(
        content: Text('Ödeme işlemi başarısız oldu.'),
        backgroundColor: Colors.red,
      ));
    }
  }
}